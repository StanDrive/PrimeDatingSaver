using System;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelTools.Core.CoreCellDataTypes;
using ExcelTools.Exceptions;

namespace ExcelTools.Core.Reading
{
    public class CoreWbReader : ICoreWbReader
    {
        private static readonly Regex ColumnRefMatcher = new Regex("[A-Z]*");

        private readonly string _filePath;
        private readonly string _wsName;

        private SpreadsheetDocument _document;
        private OpenXmlReader _reader;
        private string[] _sharedStrings;

        public CoreWbReader(string filePath, string wsName)
        {
            _filePath = filePath;
            _wsName = wsName;
        }

        public void Open()
        {
            _document = SpreadsheetDocument.Open(_filePath, false);

            Initialize(_wsName);
        }

        public bool Read()
        {
            CoreRowData row;

            if (!ReadNextNonEmptyRow(out row))
            {
                return false;
            }

            Current = row;

            return true;
        }

        public CoreRowData Current { get; private set; }

        public void Dispose()
        {
            _document?.Dispose();
        }

        #region Private
        private CoreCellData GetCellData(out string columnRef)
        {
            var reference = GetAttributeValue("r");

            var type = GetAttributeValue("t");

            var rawValue = GetCellRawValue();

            columnRef = ColumnRefMatcher.Match(reference).Value;

            return CreateCoreCellData(reference, type, rawValue);
        }

        private bool ReadNextNonEmptyRow(out CoreRowData row)
        {
            row = null;

            var nonEmptyRowFound = false;

            while (!nonEmptyRowFound && _reader.Read())
            {
                if (_reader.ElementType != typeof(Row) || !_reader.IsStartElement)
                {
                    continue;
                }

                row = new CoreRowData(_wsName);

                while (_reader.Read() && _reader.ElementType != typeof(Row))
                {
                    if (_reader.ElementType != typeof(Cell) || !_reader.IsStartElement)
                    {
                        continue;
                    }

                    string columnRef;

                    var cellData = GetCellData(out columnRef);

                    if (cellData.Value != null)
                    {
                        nonEmptyRowFound = true;
                    }

                    row.Add(columnRef, cellData);
                }
            }

            return nonEmptyRowFound;
        }

        private CoreCellData CreateCoreCellData(string cellReference, string type, string rawValue)
        {
            switch (type)
            {
                case null:
                case "inlineStr":
                    return new CoreCellDataString(rawValue);
                case "s":
                    return new CoreCellDataString(_sharedStrings[int.Parse(rawValue)]);
                case "e":
                    throw new CellContainsErrorException(_wsName, cellReference, rawValue);
                default:
                    throw new ApplicationException($"Unsupported cell type. CellReference: {cellReference}, Type: {type}, Text: {rawValue}");
            }
        }

        private string GetCellRawValue()
        {
            if (_reader.IsStartElement && _reader.IsEndElement)
            {
                return null;
            }

            while (_reader.Read() && !(_reader.ElementType == typeof(Cell) && _reader.IsEndElement))
            {
                if (_reader.ElementType == typeof(InlineString))
                {
                    _reader.Read();
                    return _reader.GetText();
                }

                if (_reader.ElementType == typeof(CellValue))
                {
                    return _reader.GetText();
                }
            }

            return null;
        }

        private string GetAttributeValue(string name)
        {
            var attribute = _reader.Attributes.FirstOrDefault(a => a.LocalName == name);

            return attribute.Value;
        }

        private string[] GetSharedStrings()
        {
            var sharedStringsPart = _document.WorkbookPart.SharedStringTablePart;

            if (sharedStringsPart?.SharedStringTable == null)
            {
                return new string[0];
            }

            return _document
                    .WorkbookPart
                    .SharedStringTablePart
                    .SharedStringTable
                    .Descendants<SharedStringItem>()
                    .Select(si => si.Text == null ? string.Empty : si.Text.InnerText)
                    .ToArray();
        }

        private void Initialize(string wsName)
        {
            _sharedStrings = GetSharedStrings();

            var sheet = string.IsNullOrWhiteSpace(wsName)
                ? _document.WorkbookPart.Workbook.Sheets.Descendants<Sheet>().FirstOrDefault()
                : _document.WorkbookPart.Workbook.Sheets.Descendants<Sheet>().FirstOrDefault(s => s.Name == wsName);

            if (sheet == null)
            {
                throw new TargetSheetDoesNotExistException(wsName);
            }

            var wsPart = (WorksheetPart)_document.WorkbookPart.GetPartById(sheet.Id);

            _reader = OpenXmlReader.Create(wsPart);
        }
        #endregion
    }
}
