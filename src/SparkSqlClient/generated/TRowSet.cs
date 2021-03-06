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
    internal partial class TRowSet : TBase
    {
        private List<TColumn> _columns;
        private byte[] _binaryColumns;
        private int _columnCount;

        public long StartRowOffset { get; set; }

        public List<TRow> Rows { get; set; }

        public List<TColumn> Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                __isset.columns = true;
                this._columns = value;
            }
        }

        public byte[] BinaryColumns
        {
            get
            {
                return _binaryColumns;
            }
            set
            {
                __isset.binaryColumns = true;
                this._binaryColumns = value;
            }
        }

        public int ColumnCount
        {
            get
            {
                return _columnCount;
            }
            set
            {
                __isset.columnCount = true;
                this._columnCount = value;
            }
        }


        public Isset __isset;
        public struct Isset
        {
            public bool columns;
            public bool binaryColumns;
            public bool columnCount;
        }

        public TRowSet()
        {
        }

        public TRowSet(long startRowOffset, List<TRow> rows) : this()
        {
            this.StartRowOffset = startRowOffset;
            this.Rows = rows;
        }

        public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
        {
            iprot.IncrementRecursionDepth();
            try
            {
                bool isset_startRowOffset = false;
                bool isset_rows = false;
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
                            if (field.Type == TType.I64)
                            {
                                StartRowOffset = await iprot.ReadI64Async(cancellationToken);
                                isset_startRowOffset = true;
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
                                    TList _list59 = await iprot.ReadListBeginAsync(cancellationToken);
                                    Rows = new List<TRow>(_list59.Count);
                                    for(int _i60 = 0; _i60 < _list59.Count; ++_i60)
                                    {
                                        TRow _elem61;
                                        _elem61 = new TRow();
                                        await _elem61.ReadAsync(iprot, cancellationToken);
                                        Rows.Add(_elem61);
                                    }
                                    await iprot.ReadListEndAsync(cancellationToken);
                                }
                                isset_rows = true;
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 3:
                            if (field.Type == TType.List)
                            {
                                {
                                    TList _list62 = await iprot.ReadListBeginAsync(cancellationToken);
                                    Columns = new List<TColumn>(_list62.Count);
                                    for(int _i63 = 0; _i63 < _list62.Count; ++_i63)
                                    {
                                        TColumn _elem64;
                                        _elem64 = new TColumn();
                                        await _elem64.ReadAsync(iprot, cancellationToken);
                                        Columns.Add(_elem64);
                                    }
                                    await iprot.ReadListEndAsync(cancellationToken);
                                }
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 4:
                            if (field.Type == TType.String)
                            {
                                BinaryColumns = await iprot.ReadBinaryAsync(cancellationToken);
                            }
                            else
                            {
                                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                            }
                            break;
                        case 5:
                            if (field.Type == TType.I32)
                            {
                                ColumnCount = await iprot.ReadI32Async(cancellationToken);
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
                if (!isset_startRowOffset)
                {
                    throw new TProtocolException(TProtocolException.INVALID_DATA);
                }
                if (!isset_rows)
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
                var struc = new TStruct("TRowSet");
                await oprot.WriteStructBeginAsync(struc, cancellationToken);
                var field = new TField();
                field.Name = "startRowOffset";
                field.Type = TType.I64;
                field.ID = 1;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteI64Async(StartRowOffset, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
                field.Name = "rows";
                field.Type = TType.List;
                field.ID = 2;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.Struct, Rows.Count), cancellationToken);
                    foreach (TRow _iter65 in Rows)
                    {
                        await _iter65.WriteAsync(oprot, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
                if (Columns != null && __isset.columns)
                {
                    field.Name = "columns";
                    field.Type = TType.List;
                    field.ID = 3;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    {
                        await oprot.WriteListBeginAsync(new TList(TType.Struct, Columns.Count), cancellationToken);
                        foreach (TColumn _iter66 in Columns)
                        {
                            await _iter66.WriteAsync(oprot, cancellationToken);
                        }
                        await oprot.WriteListEndAsync(cancellationToken);
                    }
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                if (BinaryColumns != null && __isset.binaryColumns)
                {
                    field.Name = "binaryColumns";
                    field.Type = TType.String;
                    field.ID = 4;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteBinaryAsync(BinaryColumns, cancellationToken);
                    await oprot.WriteFieldEndAsync(cancellationToken);
                }
                if (__isset.columnCount)
                {
                    field.Name = "columnCount";
                    field.Type = TType.I32;
                    field.ID = 5;
                    await oprot.WriteFieldBeginAsync(field, cancellationToken);
                    await oprot.WriteI32Async(ColumnCount, cancellationToken);
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
            var other = that as TRowSet;
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return System.Object.Equals(StartRowOffset, other.StartRowOffset)
                   && TCollections.Equals(Rows, other.Rows)
                   && ((__isset.columns == other.__isset.columns) && ((!__isset.columns) || (TCollections.Equals(Columns, other.Columns))))
                   && ((__isset.binaryColumns == other.__isset.binaryColumns) && ((!__isset.binaryColumns) || (TCollections.Equals(BinaryColumns, other.BinaryColumns))))
                   && ((__isset.columnCount == other.__isset.columnCount) && ((!__isset.columnCount) || (System.Object.Equals(ColumnCount, other.ColumnCount))));
        }

        public override int GetHashCode() {
            int hashcode = 157;
            unchecked {
                hashcode = (hashcode * 397) + StartRowOffset.GetHashCode();
                hashcode = (hashcode * 397) + TCollections.GetHashCode(Rows);
                if(__isset.columns)
                    hashcode = (hashcode * 397) + TCollections.GetHashCode(Columns);
                if(__isset.binaryColumns)
                    hashcode = (hashcode * 397) + BinaryColumns.GetHashCode();
                if(__isset.columnCount)
                    hashcode = (hashcode * 397) + ColumnCount.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("TRowSet(");
            sb.Append(", StartRowOffset: ");
            sb.Append(StartRowOffset);
            sb.Append(", Rows: ");
            sb.Append(Rows);
            if (Columns != null && __isset.columns)
            {
                sb.Append(", Columns: ");
                sb.Append(Columns);
            }
            if (BinaryColumns != null && __isset.binaryColumns)
            {
                sb.Append(", BinaryColumns: ");
                sb.Append(BinaryColumns);
            }
            if (__isset.columnCount)
            {
                sb.Append(", ColumnCount: ");
                sb.Append(ColumnCount);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}

