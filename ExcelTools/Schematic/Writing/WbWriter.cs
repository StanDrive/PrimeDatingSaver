using System.Collections.Generic;
using System.Linq;
using ExcelTools.Core;
using ExcelTools.Core.CoreCellDataTypes;
using ExcelTools.Core.Writing;

namespace ExcelTools.Schematic.Writing
{
    internal class WbWriter : IWbWriter
    {
        private readonly ICoreWbWriter _coreWbWriter;
        private readonly WbSchema _schema;

        public WbWriter(string filePath, WbSchema schema, ICoreWbFactory excelWriterFactory)
        {
            _schema = schema;
            _coreWbWriter = excelWriterFactory.Create(filePath, schema.WsNames);
        }

        public void Open()
        {
            Initialize();
        }

        public void Write(IEnumerable<RowData> rows)
        {
            _coreWbWriter.Write(ToCoreRowData(rows));
        }

        public void Dispose()
        {
            _coreWbWriter.Dispose();
        }

        public void WriteSuccessivelyNotSchematicData(IEnumerable<RowData> rows)
        {
            _coreWbWriter.Write(ToNotSchematicCoreRowData(rows));
        }

        #region Private
        private IEnumerable<CoreRowData> ToCoreRowData(IEnumerable<RowData> rows)
        {
            return rows.Select(ToCoreRow);
        }

        private IEnumerable<CoreRowData> ToNotSchematicCoreRowData(IEnumerable<RowData> rows)
        {
            return rows.Select(ToNotSchematicCoreRow);
        }

        private CoreRowData ToCoreRow(RowData row)
        {
            var wsSchema = _schema.GetWsSchema(row.SheetName);

            var cells = (from columnName in wsSchema.ColumnNames
                         let cell = row[columnName]
                         where cell != null
                         select new RefAndCell(wsSchema.GetColumnRefFromName(columnName), cell.ToCoreCellData())).ToList();

            return new CoreRowData(row.SheetName, cells);
        }

        private static CoreRowData ToNotSchematicCoreRow(RowData row)
        {
            var cells = new List<RefAndCell>();

            var columnNumber = 0;

            foreach (var data in row)
            {
                columnNumber++;

                if (data.Value != null)
                {
                    cells.Add(new RefAndCell(WsSchema.ConvertColumnNumberToColumnRef(columnNumber), data.Value.ToCoreCellData()));
                }
            }

            return new CoreRowData(row.SheetName, cells);
        }

        private RefAndCell ToRefCell(RowData row, WsSchema schema, string columnName)
        {
            return new RefAndCell(
                        schema.GetColumnRefFromName(columnName),
                        row[columnName].ToCoreCellData());
        }

        private void Initialize()
        {
            _coreWbWriter.Open();

            WriteHeadingRowsInAllSheets();
        }

        private void WriteHeadingRowsInAllSheets()
        {
            foreach (var wsSchema in _schema.WsNames.Select(wsName => _schema.GetWsSchema(wsName)))
            {
                _coreWbWriter.Write(new List<CoreRowData> { GetHeadingRowData(wsSchema) });
            }
        }

        private CoreRowData GetHeadingRowData(WsSchema wsSchema)
        {
            var columnNames = wsSchema.ColumnNames;

            return new CoreRowData(
                            wsSchema.SheetName,
                            columnNames.Select(columnName => Pair(wsSchema.GetColumnRefFromName(columnName), new CoreCellDataHeaderString(columnName))));
        }

        private static RefAndCell Pair(string key, CoreCellData value)
        {
            return new RefAndCell(key, value);
        }
        #endregion
    }
}