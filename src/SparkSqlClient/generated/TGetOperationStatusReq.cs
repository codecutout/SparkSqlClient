/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;

namespace SparkSqlClient.generated
{
    internal partial class TGetOperationStatusReq : TBase
    {
        private bool _getProgressUpdate;

        public TOperationHandle OperationHandle { get; set; }

        public bool GetProgressUpdate
        {
            get
            {
                return _getProgressUpdate;
            }
            set
            {
                __isset.getProgressUpdate = true;
                this._getProgressUpdate = value;
            }
        }


        public Isset __isset;
        public struct Isset
        {
            public bool getProgressUpdate;
        }

        public TGetOperationStatusReq()
        {
        }

        public TGetOperationStatusReq(TOperationHandle operationHandle) : this()
        {
            this.OperationHandle = operationHandle;
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                bool isset_operationHandle = false;
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
                            if (field.Type == TType.Struct)
                            {
                                OperationHandle = new TOperationHandle();
                                await OperationHandle.ReadAsync(iprot, cancellationToken);
                                isset_operationHandle = true;
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 2:
                            if (field.Type == TType.Bool)
                            {
                                GetProgressUpdate = await iprot.ReadBoolAsync(cancellationToken);
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
                if (!isset_operationHandle)
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
                var struc = new TStruct("TGetOperationStatusReq");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();
                field.Name = "operationHandle";
                field.Type = TType.Struct;
                field.ID = 1;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await OperationHandle.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
                if (__isset.getProgressUpdate)
                {
                    field.Name = "getProgressUpdate";
                    field.Type = TType.Bool;
                    field.ID = 2;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteBoolAsync(GetProgressUpdate, cancellationToken);
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
            var other = that as TGetOperationStatusReq;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return System.Object.Equals(OperationHandle, other.OperationHandle)
                   && ((__isset.getProgressUpdate == other.__isset.getProgressUpdate) && ((!__isset.getProgressUpdate) || (System.Object.Equals(GetProgressUpdate, other.GetProgressUpdate))));
        }

        public override int GetHashCode() {
            int hashcode = 157;
            unchecked {
                hashcode = (hashcode * 397) + OperationHandle.GetHashCode();
                if(__isset.getProgressUpdate)
                    hashcode = (hashcode * 397) + GetProgressUpdate.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("TGetOperationStatusReq(");
            sb.Append(", OperationHandle: ");
            sb.Append(OperationHandle== null ? "<null>" : OperationHandle.ToString());
            if (__isset.getProgressUpdate)
            {
                sb.Append(", GetProgressUpdate: ");
                sb.Append(GetProgressUpdate);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}

