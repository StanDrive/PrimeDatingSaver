using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Core.CoreCellDataTypes;

namespace ExcelTools.Core.Writing
{
    internal class AsyncWsWriter : IWsWriter
    {
        private const double DefaultColumnWidth = 30;
        private const double DefaultRowHeight = 12;
        private static readonly TimeSpan TakeWaitTimeout = TimeSpan.FromSeconds(1);

        private readonly OpenXmlWriter _writer;

        private uint _currentRowIndex = 1;
        private readonly BlockingCollection<CoreRowData> _rowsToWrite = new BlockingCollection<CoreRowData>(new ConcurrentQueue<CoreRowData>(), 100);
        private Task _rowsWriter;

        public AsyncWsWriter(WorksheetPart worksheetPart)
        {
            _writer = OpenXmlWriter.Create(worksheetPart);

            WriteWorksheetStartElements();

            RunWritingTask();
        }

        public void WriteRow(CoreRowData row)
        {
            _rowsToWrite.Add(row);
        }

        public void Dispose()
        {
            _rowsToWrite.CompleteAdding();
            _rowsWriter.Wait();

            WriteWorksheetEndElements();

            _writer.Close();
        }

        #region Private
        private void RunWritingTask()
        {
            _rowsWriter = Task.Run(() =>
            {
                while (!_rowsToWrite.IsCompleted)
                {
                    CoreRowData rowData;

                    if (_rowsToWrite.TryTake(out rowData, TakeWaitTimeout))
                    {
                        WriteRowImpl(rowData);
                    }
                }
            });
        }

        private void WriteRowImpl(CoreRowData row)
        {
            try
            {
                _writer.WriteStartElement(new Row { RowIndex = _currentRowIndex });

                foreach (var refCell in row.RefCells)
                {
                    _writer.WriteElement(CreateCell(refCell.ColumnRef, refCell.Cell));
                }

                _writer.WriteEndElement();
            }
            finally
            {
                _currentRowIndex++;
            }
        }

        private void WriteWorksheetStartElements()
        {
            _writer.WriteStartElement(new Worksheet());
            _writer.WriteElement(new SheetFormatProperties { DefaultColumnWidth = DefaultColumnWidth, DefaultRowHeight = DefaultRowHeight });
            _writer.WriteStartElement(new SheetData());
        }

        private void WriteWorksheetEndElements()
        {
            _writer.WriteEndElement();
            _writer.WriteEndElement();
        }

        private Cell CreateCell(string columnRef, CoreCellData cellData)
        {
            var result = new Cell();

            CellValues type = cellData.DataType;

            switch (type)
            {
                case CellValues.Number:
                case CellValues.Date:
                    // Don't use CellValues.Date, CellValues.Number. Excel 2010 doesn't support them
                    result.CellValue = new CellValue(cellData.Value);
                    break;
                case CellValues.InlineString:
                    result.DataType = new EnumValue<CellValues>(CellValues.InlineString);
                    result.InlineString = new InlineString { Text = new Text(cellData.Value) };
                    break;
                default:
                    throw new NotSupportedException($"Data type isn't currently supported. DataType: {type}");
            }

            result.CellReference = $"{columnRef}{_currentRowIndex}";
            result.StyleIndex = (uint)cellData.Style;

            return result;
        }
        #endregion
    }
}
