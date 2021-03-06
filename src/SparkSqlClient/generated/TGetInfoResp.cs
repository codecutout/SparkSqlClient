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
    internal partial class TGetInfoResp : TBase
    {

        public TStatus Status { get; set; }

        public TGetInfoValue InfoValue { get; set; }

        public TGetInfoResp()
        {
        }

        public TGetInfoResp(TStatus status, TGetInfoValue infoValue) : this()
        {
            this.Status = status;
            this.InfoValue = infoValue;
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                bool isset_status = false;
                bool isset_infoValue = false;
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
                                Status = new TStatus();
                                await Status.ReadAsync(iprot, cancellationToken);
                                isset_status = true;
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 2:
                            if (field.Type == TType.Struct)
                            {
                                InfoValue = new TGetInfoValue();
                                await InfoValue.ReadAsync(iprot, cancellationToken);
                                isset_infoValue = true;
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
                if (!isset_status)
                {
                    throw new TProtocolException(TProtocolException.INVALID_DATA);
                }
                if (!isset_infoValue)
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
                var struc = new TStruct("TGetInfoResp");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();
                field.Name = "status";
                field.Type = TType.Struct;
                field.ID = 1;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await Status.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
                field.Name = "infoValue";
                field.Type = TType.Struct;
                field.ID = 2;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await InfoValue.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
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
            var other = that as TGetInfoResp;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return System.Object.Equals(Status, other.Status)
                   && System.Object.Equals(InfoValue, other.InfoValue);
        }

        public override int GetHashCode() {
            int hashcode = 157;
            unchecked {
                hashcode = (hashcode * 397) + Status.GetHashCode();
                hashcode = (hashcode * 397) + InfoValue.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("TGetInfoResp(");
            sb.Append(", Status: ");
            sb.Append(Status== null ? "<null>" : Status.ToString());
            sb.Append(", InfoValue: ");
            sb.Append(InfoValue== null ? "<null>" : InfoValue.ToString());
            sb.Append(")");
            return sb.ToString();
        }
    }
}

