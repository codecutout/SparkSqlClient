/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */

using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;

namespace SparkSqlClient.generated
{
    internal partial class TStatus : TBase
    {
        private List<string> _infoMessages;
        private string _sqlState;
        private int _errorCode;
        private string _errorMessage;

        /// <summary>
        /// 
        /// <seealso cref="TStatusCode"/>
        /// </summary>
        public TStatusCode StatusCode { get; set; }

        public List<string> InfoMessages
        {
            get
            {
                return _infoMessages;
            }
            set
            {
                __isset.infoMessages = true;
                this._infoMessages = value;
            }
        }

        public string SqlState
        {
            get
            {
                return _sqlState;
            }
            set
            {
                __isset.sqlState = true;
                this._sqlState = value;
            }
        }

        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                __isset.errorCode = true;
                this._errorCode = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                __isset.errorMessage = true;
                this._errorMessage = value;
            }
        }


        public Isset __isset;
        public struct Isset
        {
            public bool infoMessages;
            public bool sqlState;
            public bool errorCode;
            public bool errorMessage;
        }

        public TStatus()
        {
        }

        public TStatus(TStatusCode statusCode) : this()
        {
            this.StatusCode = statusCode;
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                bool isset_statusCode = false;
                TField field;
                await iprot.ReadStructBeginAsync(cancellationToken);
                while (true)
                {
                    field = await iprot.ReadFieldBeginAsync(cancellationToken);
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }

                    switch (field.ID)
                    {
                        case 1:
                            if (field.Type == TType.I32)
                            {
                                StatusCode = (TStatusCode)await iprot.ReadI32Async(cancellationToken);
                                isset_statusCode = true;
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 2:
                            if (field.Type == TType.List)
                            {
                                {
                                    TList _list67 = await iprot.ReadListBeginAsync(cancellationToken);
                                    InfoMessages = new List<string>(_list67.Count);
                                    for(int _i68 = 0; _i68 < _list67.Count; ++_i68)
                                    {
                                        string _elem69;
                                        _elem69 = await iprot.ReadStringAsync(cancellationToken);
                                        InfoMessages.Add(_elem69);
                                    }
                                    await iprot.ReadListEndAsync(cancellationToken);
                                }
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 3:
                            if (field.Type == TType.String)
                            {
                                SqlState = await iprot.ReadStringAsync(cancellationToken);
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 4:
                            if (field.Type == TType.I32)
                            {
                                ErrorCode = await iprot.ReadI32Async(cancellationToken);
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 5:
                            if (field.Type == TType.String)
                            {
                                ErrorMessage = await iprot.ReadStringAsync(cancellationToken);
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        default: 
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            break;
                    }

                    await iprot.ReadFieldEndAsync(cancellationToken);
                }

                await iprot.ReadStructEndAsync(cancellationToken);
                if (!isset_statusCode)
                {
                    throw new TProtocolException(TProtocolException.INVALID_DATA);
                }
            }
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("TStatus");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();
                field.Name = "statusCode";
                field.Type = TType.I32;
                field.ID = 1;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteI32Async((int)StatusCode, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
                if (InfoMessages != null && __isset.infoMessages)
                {
                    field.Name = "infoMessages";
                    field.Type = TType.List;
                    field.ID = 2;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    {
                        await oprot.WriteListBeginAsync(new TList(TType.String, InfoMessages.Count), cancellationToken);
                        foreach (string _iter70 in InfoMessages)
                        {
                            await oprot.WriteStringAsync(_iter70, cancellationToken);
                        }
                        await oprot.WriteListEndAsync(cancellationToken);
                    }
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                if (SqlState != null && __isset.sqlState)
                {
                    field.Name = "sqlState";
                    field.Type = TType.String;
                    field.ID = 3;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteStringAsync(SqlState, cancellationToken);
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                if (__isset.errorCode)
                {
                    field.Name = "errorCode";
                    field.Type = TType.I32;
                    field.ID = 4;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteI32Async(ErrorCode, cancellationToken);
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                if (ErrorMessage != null && __isset.errorMessage)
                {
                    field.Name = "errorMessage";
                    field.Type = TType.String;
                    field.ID = 5;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteStringAsync(ErrorMessage, cancellationToken);
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                await oprot.WriteFieldStopAsync(cancellationToken);
                await oprot.WriteStructEndAsync(cancellationToken);
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override bool Equals(object that)
        {
            var other = that as TStatus;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return System.Object.Equals(StatusCode, other.StatusCode)
                   && ((__isset.infoMessages == other.__isset.infoMessages) && ((!__isset.infoMessages) || (TCollections.Equals(InfoMessages, other.InfoMessages))))
                   && ((__isset.sqlState == other.__isset.sqlState) && ((!__isset.sqlState) || (System.Object.Equals(SqlState, other.SqlState))))
                   && ((__isset.errorCode == other.__isset.errorCode) && ((!__isset.errorCode) || (System.Object.Equals(ErrorCode, other.ErrorCode))))
                   && ((__isset.errorMessage == other.__isset.errorMessage) && ((!__isset.errorMessage) || (System.Object.Equals(ErrorMessage, other.ErrorMessage))));
        }

        public override int GetHashCode() {
            int hashcode = 157;
            unchecked {
                hashcode = (hashcode * 397) + StatusCode.GetHashCode();
                if(__isset.infoMessages)
                    hashcode = (hashcode * 397) + TCollections.GetHashCode(InfoMessages);
                if(__isset.sqlState)
                    hashcode = (hashcode * 397) + SqlState.GetHashCode();
                if(__isset.errorCode)
                    hashcode = (hashcode * 397) + ErrorCode.GetHashCode();
                if(__isset.errorMessage)
                    hashcode = (hashcode * 397) + ErrorMessage.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("TStatus(");
            sb.Append(", StatusCode: ");
            sb.Append(StatusCode);
            if (InfoMessages != null && __isset.infoMessages)
            {
                sb.Append(", InfoMessages: ");
                sb.Append(InfoMessages);
            }
            if (SqlState != null && __isset.sqlState)
            {
                sb.Append(", SqlState: ");
                sb.Append(SqlState);
            }
            if (__isset.errorCode)
            {
                sb.Append(", ErrorCode: ");
                sb.Append(ErrorCode);
            }
            if (ErrorMessage != null && __isset.errorMessage)
            {
                sb.Append(", ErrorMessage: ");
                sb.Append(ErrorMessage);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}

