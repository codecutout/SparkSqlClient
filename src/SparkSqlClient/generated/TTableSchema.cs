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
    internal partial class TTableSchema : TBase
    {

        public List<TColumnDesc> Columns { get; set; }

        public TTableSchema()
        {
        }

        public TTableSchema(List<TColumnDesc> columns) : this()
        {
            this.Columns = columns;
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                bool isset_columns = false;
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
                                    TList _list19 = await iprot.ReadListBeginAsync(cancellationToken);
                                    Columns = new List<TColumnDesc>(_list19.Count);
                                    for(int _i20 = 0; _i20 < _list19.Count; ++_i20)
                                    {
                                        TColumnDesc _elem21;
                                        _elem21 = new TColumnDesc();
                                        await _elem21.ReadAsync(iprot, cancellationToken);
                                        Columns.Add(_elem21);
                                    }
                                    await iprot.ReadListEndAsync(cancellationToken);
                                }
                                isset_columns = true;
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
                if (!isset_columns)
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
                var struc = new TStruct("TTableSchema");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();
                field.Name = "columns";
                field.Type = TType.List;
                field.ID = 1;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.Struct, Columns.Count), cancellationToken);
                    foreach (TColumnDesc _iter22 in Columns)
                    {
                        await _iter22.WriteAsync(oprot, cancellationToken);
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
            var other = that as TTableSchema;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return TCollections.Equals(Columns, other.Columns);
        }

        public override int GetHashCode() {
            int hashcode = 157;
            unchecked {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(Columns);
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("TTableSchema(");
            sb.Append(", Columns: ");
            sb.Append(Columns);
            sb.Append(")");
            return sb.ToString();
        }
    }
}

