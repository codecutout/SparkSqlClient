﻿using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SparkSqlClient.exceptions;
using SparkSqlClient.generated;

namespace SparkSqlClient
{
    /// <summary>
    /// Represents a command against a spark server.
    /// </summary>
    public class SparkCommand : DbCommand
    {
        private string _commandText;
        public override string CommandText
        {
            get => _commandText;
            set => _commandText = value;
        }

        public override int CommandTimeout { get; set; } = -1;

        protected TimeSpan? CommandTimeoutTimespan =>
            CommandTimeout <= 0 ? null : (TimeSpan?) TimeSpan.FromSeconds(CommandTimeout);

        public override CommandType CommandType
        {
            get => CommandType.Text;
            set 
            {
                if (value != CommandType)
                    throw new NotSupportedException($"{nameof(SparkCommand)} does not support setting {nameof(CommandType)} to {value}");
            }
        }
        public override bool DesignTimeVisible
        {
            get => false;
            set
            {
                if (value != DesignTimeVisible)
                    throw new NotSupportedException($"{nameof(SparkCommand)} does not support setting {nameof(DesignTimeVisible)}");
            }
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get => UpdateRowSource.None;
            set
            {
                if (value != UpdatedRowSource)
                    throw new NotSupportedException($"{nameof(SparkCommand)} does not support setting {nameof(UpdatedRowSource)}");
            }
        }

        private SparkConnection _sparkConnection;
        protected override DbConnection DbConnection
        {
            get => _sparkConnection;
            set =>
                _sparkConnection = value as SparkConnection
                                   ?? throw new ArgumentException(
                                       $"{nameof(DbConnection)} must be set to a value of type '{typeof(SparkConnection)}'")
            ;

        }

        protected override DbParameterCollection DbParameterCollection { get; } = new SparkParameterCollection();
    

        protected override DbTransaction DbTransaction
        {
            get => null;
            set
            {
                if (value != DbTransaction)
                    throw new NotSupportedException($"{nameof(SparkCommand)} does not support setting {nameof(DbTransaction)}");
            }
        }

        public SparkCommand(SparkConnection connection) : base()
        {
            _sparkConnection = connection;
        }

        public override void Cancel()
        {
            throw new NotSupportedException($"{nameof(SparkCommand)} does not support canceling operations");
        }

        public override int ExecuteNonQuery()
        {
            return ExecuteNonQueryAsync(CancellationToken.None).GetAwaiter().GetResult();
        }

        public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            if (_sparkConnection.State != ConnectionState.Open) throw new InvalidOperationException("Session must be opened before executing a statement");

            var operationHandle = await ExecuteStatement(
                _sparkConnection.Client,
                _sparkConnection.SessionHandle,
                CommandText,
                DbParameterCollection,
                CommandTimeoutTimespan,
                cancellationToken).ConfigureAwait(false);

            await CloseStatement(_sparkConnection.Client, operationHandle, CancellationToken.None).ConfigureAwait(false);

            // Although in the schema ModifiedRowCount does not appear to be ever set still returning it if its there
            return operationHandle.__isset.modifiedRowCount ? (int)operationHandle.ModifiedRowCount : -1;
        }

        public override object ExecuteScalar()
        {
            return ExecuteScalarAsync(CancellationToken.None).GetAwaiter().GetResult();
        }

        public override async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            if (_sparkConnection.State != ConnectionState.Open) throw new InvalidOperationException("Session must be opened before executing a statement");

            await using var reader = await ExecuteDbDataReaderAsync(CommandBehavior.Default, cancellationToken).ConfigureAwait(false);
            
            if(!await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                throw new InvalidOperationException("Unable to read scaler result");

            return reader.GetValue(0);
        }

        public override void Prepare()
        {
            throw new NotSupportedException($"{nameof(SparkCommand)} does not support preparing statements");
        }

        protected override DbParameter CreateDbParameter()
        {
            return new SparkParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return ExecuteDbDataReaderAsync(behavior, CancellationToken.None).GetAwaiter().GetResult();
        }

        protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            if (_sparkConnection.State != ConnectionState.Open) throw new InvalidOperationException("Session must be opened before executing a statement");

            var operationHandle = await ExecuteStatement(
                _sparkConnection.Client,
                _sparkConnection.SessionHandle, 
                CommandText, 
                DbParameterCollection,
                CommandTimeoutTimespan,
                cancellationToken).ConfigureAwait(false);

            var metadataResponse = await _sparkConnection.Client.GetResultSetMetadataAsync(new TGetResultSetMetadataReq()
            {
                OperationHandle = operationHandle,
            }, cancellationToken).ConfigureAwait(false);
            SparkOperationException.ThrowIfInvalidStatus(metadataResponse.Status);
            
            return new SparkDataReader(
                _sparkConnection.Client, 
                _sparkConnection.SessionHandle,
                operationHandle,
                metadataResponse.Schema,
                async ()=> await CloseStatement(_sparkConnection.Client, operationHandle, CancellationToken.None).ConfigureAwait(false));
        }

        private static CancellationToken BuildTimeoutCancellationToken(TimeSpan? timespan, CancellationToken cancellationToken)
        {
            if (!timespan.HasValue) return cancellationToken;

            var timeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutTokenSource.CancelAfter(timespan.Value);
            return timeoutTokenSource.Token;
        }

        private static async Task<TOperationHandle> ExecuteStatement(
            TCLIService.IAsync client, 
            TSessionHandle sessionHandle, 
            string commandText, 
            DbParameterCollection parameters, 
            TimeSpan? timeout,
            CancellationToken cancellationToken)
        {
            var parameterizedCommandText = ReplaceParameters(commandText, parameters);
            cancellationToken = BuildTimeoutCancellationToken(timeout, cancellationToken);

            var executeStatementResponse = await client.ExecuteStatementAsync(new TExecuteStatementReq
            {
                SessionHandle = sessionHandle,
                Statement = parameterizedCommandText,
                RunAsync = true,
                
            }, cancellationToken).ConfigureAwait(false);
            SparkOperationException.ThrowIfInvalidStatus(executeStatementResponse.Status);

            await WaitUntilOperationSuccess(client, executeStatementResponse.OperationHandle, cancellationToken).ConfigureAwait(false);

            return executeStatementResponse.OperationHandle;
        }

        private static async Task WaitUntilOperationSuccess(TCLIService.IAsync client, TOperationHandle operationHandle, CancellationToken cancellationToken)
        {
            var exponentialPolling = new[]
            {
                TimeSpan.Zero,
                TimeSpan.FromSeconds(0.5),
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
            }.Concat(Enumerable.Repeat(TimeSpan.FromSeconds(4), Int32.MaxValue));

            foreach (var delay in exponentialPolling)
            {
                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);

                var getOperationStatusResponse = await client.GetOperationStatusAsync(new TGetOperationStatusReq()
                {
                    OperationHandle = operationHandle,
                }, cancellationToken).ConfigureAwait(false);
                SparkOperationException.ThrowIfInvalidStatus(getOperationStatusResponse.Status);

                if (getOperationStatusResponse.OperationState == TOperationState.FINISHED_STATE)
                    return;
                
                if (new[]{TOperationState.RUNNING_STATE, TOperationState.PENDING_STATE, TOperationState.INITIALIZED_STATE}.Contains(getOperationStatusResponse.OperationState))
                    continue;

                if (getOperationStatusResponse.__isset.errorMessage)
                    throw new SparkOperationException(getOperationStatusResponse.ErrorMessage, Enumerable.Empty<string>());

                throw new SparkOperationException($"Operation has failed with status '{getOperationStatusResponse.OperationState}'", Enumerable.Empty<string>());
            }
        }

        private static async Task CloseStatement(TCLIService.IAsync client, TOperationHandle operationHandle, CancellationToken cancellationToken)
        {
            var closeResponse = await client.CloseOperationAsync(new TCloseOperationReq()
            {
                OperationHandle = operationHandle
            }, cancellationToken).ConfigureAwait(false);
            SparkOperationException.ThrowIfInvalidStatus(closeResponse.Status);
        }

        private static string ReplaceParameters(string command, DbParameterCollection parameters)
        {
            Func<char, string> matchingQuote = quote => $@"{quote}(?:[^{quote}\\]|\\.)*{quote}";
            var regex = new Regex(string.Join("|", new[]
            {
                matchingQuote('`'),
                matchingQuote('"'),
                matchingQuote('\''),
                "@(\\w+)"
            }));

            return regex.Replace(command, match =>
            {
                // Do nothing if did not match on the @someValue part of the regex
                if (match.Groups.Count != 2 || !match.Groups[1].Success)
                    return match.Groups[0].Value;

                var parameterName = match.Groups[1].Value;
                var parameterIndex = parameters.IndexOf(parameterName);
                if (parameterIndex < 0)
                    throw new MissingParameterException(parameterName);
                var parameter = parameters[parameterIndex];

                switch (parameter.Value)
                {
                    case string str:
                        return $@"'{str.Replace("'", "\\'")}'";
                    case DateTime dt when dt.TimeOfDay == TimeSpan.Zero:
                        return $"'{dt:yyyy-MM-dd}'";
                    case DateTime dt:
                        return $"'{dt:yyyy-MM-dd HH:mm:ss.fff}'";
                    case byte b:
                        // Spark bytes are signed while dotnet's are not. Need to force a CAST
                        return $"CAST({b} AS TINYINT)";
                    case byte[] ba:
                        // Encode binary as Base64
                        return $"UNBASE64('{Convert.ToBase64String(ba)}')";
                    default:
                        return parameter.Value.ToString();
                }
            });
        }
    
    }
}
