using System;
using System.Collections.Generic;
using System.Linq;
using ExcelTools.Core;
using ExcelTools.Core.Reading;
using ExcelTools.Exceptions;

namespace ExcelTools.Schematic.Reading
{
    public class WbReader : IWbReader
    {
        private readonly string _filePath;
        private readonly ICoreWbReaderFactory _coreWbReaderFactory;
        private ICoreWbReader _coreWbReader;

        public RowData Current { get; private set; }

        public WbReader(string filePath, ICoreWbReaderFactory coreWbReaderFactory)
        {
            _filePath = filePath;
            _coreWbReaderFactory = coreWbReaderFactory;
        }

        public void Open(WbSchema wbSchema)
        {
            var wsSchema = GetSingleWsSchema(wbSchema);

            _coreWbReader = _coreWbReaderFactory.Create(_filePath, wsSchema.SheetName);
            _coreWbReader.Open();
        }

        public bool Read()
        {
            if (!_coreWbReader.Read())
            {
                return false;
            }

            Current = ToDpRowData(_coreWbReader.Current);

            return true;
        }

        public bool Skip(int count)
        {
            for (var i = 0; i < count; i++)
            {
                if (!_coreWbReader.Read())
                {
                    return false;
                }
            }

            return true;
        }

        public void SkipHeader(WbSchema schema)
        {
            var wsSchema = GetSingleWsSchema(schema);

            ValidateAndSkipHeadingRow(wsSchema);
        }

        public bool Read(WbSchema schema)
        {
            var wsSchema = GetSingleWsSchema(schema);

            if (!_coreWbReader.Read())
            {
                return false;
            }

            Current = ToDpRowData(wsSchema, _coreWbReader.Current);

            return true;
        }

        public void Dispose()
        {
            _coreWbReader.Dispose();
        }

        #region Private
        private static RowData ToDpRowData(WsSchema wsSchema, CoreRowData rowData)
        {
            var result = new RowData(wsSchema.SheetName);

            foreach (var columnName in wsSchema.ColumnNames)
            {
                var reference = wsSchema.GetColumnRefFromName(columnName);

                if (rowData.RefCells.Exists(r => r.ColumnRef == reference))
                {
                    result.Add(columnName, rowData.RefCells.Find(r => r.ColumnRef == reference).Cell.ToCellData());
                }
            }
            return result;
        }

        private static RowData ToDpRowData(CoreRowData rowData)
        {
            var result = new RowData(null);

            foreach (var refCell in rowData.RefCells)
            {
                result.Add(
                        refCell.ColumnRef,
                        refCell.Cell.ToCellData());
            }

            return result;
        }


        private void ValidateAndSkipHeadingRow(WsSchema wsSchema)
        {
            if (!_coreWbReader.Read())
            {
                throw new TargetSheetDoesNotContainHeadingRowException(wsSchema.SheetName);
            }

            var headingRow = _coreWbReader.Current;

            ValidateHeadingRow(wsSchema, headingRow);
        }

        private static void ValidateHeadingRow(WsSchema wsSchema, CoreRowData headingRow)
        {
            var headingRowColumns = new HashSet<string>(
                headingRow.RefCells.Where(c => c.Cell.Value != null).
                    Select(c => c.Cell.Value.Trim()));

            foreach (var schemaColumn in wsSchema.ColumnNames.Where(schemaColumn => !headingRowColumns.Contains(schemaColumn)))
            {
                throw new TargetSheetHeadingRowIsInvalidException(wsSchema.SheetName, schemaColumn);
            }
        }

        private static WsSchema GetSingleWsSchema(WbSchema schema)
        {
            if (schema == null || schema.WsNames.Length != 1)
            {
                throw new ArgumentException("Reader requires schema with a single worksheet");
            }

            return schema.GetWsSchema(schema.WsNames[0]);
        }
        #endregion

    }
}
