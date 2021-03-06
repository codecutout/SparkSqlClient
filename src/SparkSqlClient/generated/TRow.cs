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
    internal partial class TRow : TBase
    {

        public List<TColumnValue> ColVals { get; set; }

        public TRow()
        {
        }

        public TRow(List<TColumnValue> colVals) : this()
        {
            this.ColVals = colVals;
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                bool isset_colVals = false;
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
                            if (field.Type == TType.List)
                            {
                                {
                                    TList _list23 = await iprot.ReadListBeginAsync(cancellationToken);
                                    ColVals = new List<TColumnValue>(_list23.Count);
                                    for(int _i24 = 0; _i24 < _list23.Count; ++_i24)
                                    {
                                        TColumnValue _elem25;
                                        _elem25 = new TColumnValue();
                                        await _elem25.ReadAsync(iprot, cancellationToken);
                                        ColVals.Add(_elem25);
                                    }
                                    await iprot.ReadListEndAsync(cancellationToken);
                                }
                                isset_colVals = true;
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
                if (!isset_colVals)
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
                var struc = new TStruct("TRow");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();
                field.Name = "colVals";
                field.Type = TType.List;
                field.ID = 1;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.Struct, ColVals.Count), cancellationToken);
                    foreach (TColumnValue _iter26 in ColVals)
                    {
                        await _iter26.WriteAsync(oprot, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
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
            var other = that as TRow;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return TCollections.Equals(ColVals, other.ColVals);
        }

        public override int GetHashCode() {
            int hashcode = 157;
            unchecked {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(ColVals);
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("TRow(");
            sb.Append(", ColVals: ");
            sb.Append(ColVals);
            sb.Append(")");
            return sb.ToString();
        }
    }
}

