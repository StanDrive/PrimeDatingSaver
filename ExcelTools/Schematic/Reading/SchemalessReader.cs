using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ExcelTools.Core.Reading;
using Newtonsoft.Json;

namespace ExcelTools.Schematic.Reading
{
    public class SchemalessReader : ISchemalessReader
    {
        private readonly string _filePath;
        private readonly ICoreWbReaderFactory _coreWbReaderFactory;
        private ICoreWbReader _coreWbReader;

        public SchemalessReader(string filePath, ICoreWbReaderFactory coreWbReaderFactory)
        {
            _filePath = filePath;
            _coreWbReaderFactory = coreWbReaderFactory;
        }

        /// <summary>
        /// Opens the specified sheet name. If omitted, then will take the first one.
        /// </summary>
        /// <param name="sheetName">Name of the sheet.</param>
        public void Open(string sheetName)
        {
            _coreWbReader = _coreWbReaderFactory.Create(_filePath, sheetName);

            _coreWbReader.Open();
        }

        /// <summary>
        /// Skips the specified count of rows.
        /// Empty rows didn't are not taken into account.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public void Skip(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _coreWbReader.Read();
            }
        }

        /// <summary>
        /// Reads this worksheet and return json result.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Read()
        {
            _coreWbReader.Read();

            var headers = GetHeader();

            var resultTable = new DataTable();

            foreach (var header in headers)
            {
                resultTable.Columns.Add(header.Cell.Value, typeof(string));
            }

            while (_coreWbReader.Read())
            {
                var row = resultTable.NewRow();

                foreach (var refAndCell in headers)
                {
                    row[refAndCell.Cell.Value] =
                        _coreWbReader.Current.RefCells.FirstOrDefault(t => t.ColumnRef == refAndCell.ColumnRef)?
                            .Cell?.Value;
                }

                resultTable.Rows.Add(row);
            }

            return JsonConvert.SerializeObject(resultTable);
        }

        /// <summary>
        /// Extracts parts of a spreadsheet rows and returns the extracted parts in a new json.
        /// </summary>
        /// <param name="begin">The position where to begin the extraction. First character is at position 0.</param>
        /// <param name="end">The position (up to, but not including) where to end the extraction.</param>
        /// <returns>System.String.</returns>
        public string Slice(int begin, int end)
        {
            if (begin < 0)
            {
                throw new ArgumentException("Argument 'begin' should be positive integer value");
            }

            if (end < 0)
            {
                throw new ArgumentException("Argument 'end' should be positive integer value");
            }

            if (begin >= end)
            {
                throw new ArgumentException("Argument 'begin' should be lower that 'end' value");
            }

            _coreWbReader.Read();

            var rowIndex = -1;

            var headers = GetHeader();

            var resultTable = new DataTable();

            foreach (var header in headers)
            {
                resultTable.Columns.Add(header.Cell.Value, typeof(string));
            }

            while (_coreWbReader.Read())
            {
                rowIndex++;

                if (begin > rowIndex || rowIndex >= end) continue;

                var row = resultTable.NewRow();

                foreach (var refAndCell in headers)
                {
                    row[refAndCell.Cell.Value] =
                        _coreWbReader.Current.RefCells.FirstOrDefault(t => t.ColumnRef == refAndCell.ColumnRef)?
                            .Cell?.Value;
                }

                resultTable.Rows.Add(row);

                if (rowIndex + 1 == end)
                {
                    break;
                }
            }

            return JsonConvert.SerializeObject(resultTable);
        }

        /// <summary>
        /// Extracts parts of a spreadsheet rows and returns the extracted parts in a new json.
        /// Selects all rows from the start-position to the end of the worksheet.
        /// </summary>
        /// <param name="begin">The position where to begin the extraction. First character is at position 0.</param>
        /// <returns>System.String.</returns>
        public string Slice(int begin)
        {
            if (begin < 0)
            {
                throw new ArgumentException("Argument 'begin' should be positive integer value");
            }

            _coreWbReader.Read();

            var rowIndex = -1;

            var headers = GetHeader();

            var resultTable = new DataTable();

            foreach (var header in headers)
            {
                resultTable.Columns.Add(header.Cell.Value, typeof(string));
            }

            while (_coreWbReader.Read())
            {
                rowIndex++;

                if (begin <= rowIndex)
                {
                    var row = resultTable.NewRow();

                    foreach (var refAndCell in headers)
                    {
                        row[refAndCell.Cell.Value] =
                            _coreWbReader.Current.RefCells.FirstOrDefault(t => t.ColumnRef == refAndCell.ColumnRef)?
                                .Cell?.Value;
                    }

                    resultTable.Rows.Add(row);
                }
            }

            return JsonConvert.SerializeObject(resultTable);
        }


        private List<RefAndCell> GetHeader()
        {
            var header = new List<RefAndCell>();

            foreach (
                var cellData in
                    _coreWbReader.Current.RefCells.Select(refCell => refCell)
                        .Where(cellData => !string.IsNullOrWhiteSpace(cellData.Cell.ToCellData().ValueStr.Trim())))
            {
                if (header.Any(x => x.Cell.Value == cellData.Cell.Value))
                {
                    throw new ApplicationException($"Spreadsheet heading contain duplicate of column'{cellData.Cell.Value}'");
                }

                header.Add(cellData);
            }

            return header;
        }
    }
}
