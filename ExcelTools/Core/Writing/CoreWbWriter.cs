using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ExcelTools.Core.Writing
{
    internal class CoreWbWriter : ICoreWbWriter
    {
        private readonly IWsWriterFactory _worksheetWriterFactory;
        private readonly string _filePath;
        private readonly string[] _sheetNames;

        private SpreadsheetDocument _document;
        private Dictionary<string, IWsWriter> _worksheetNameToWriterMapping;

        public CoreWbWriter(string filePath, string[] sheetNames, IWsWriterFactory worksheetWriterFactory)
        {
            _worksheetWriterFactory = worksheetWriterFactory;
            _filePath = filePath;
            _sheetNames = sheetNames;
        }

        public void Open()
        {
            CreateAndInitializeDocument(_filePath, _sheetNames);

            InitializeWriters(_sheetNames);
        }

        public void Write(IEnumerable<CoreRowData> rows)
        {
            foreach (var row in rows)
            {
                var writer = _worksheetNameToWriterMapping[row.SheetName];

                writer.WriteRow(row);
            }
        }

        public void Dispose()
        {
            AddStylePart();

            foreach (var writer in _worksheetNameToWriterMapping.Values)
            {
                writer.Dispose();
            }

            _document.WorkbookPart.Workbook.Save();
            _document.Dispose();
        }

        #region Private
        private void CreateAndInitializeDocument(string filePath, string[] sheetNames)
        {
            if (!sheetNames.Any())
            {
                throw new ArgumentException("Should contain at least one sheet");
            }

            _document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

            var workbookPart = _document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            _document.WorkbookPart.AddNewPart<SharedStringTablePart>();
            _document.WorkbookPart.SharedStringTablePart.SharedStringTable = new SharedStringTable();

            var sheets = workbookPart.Workbook.AppendChild(new Sheets());

            for (var i = 0; i < sheetNames.Length; i++)
            {
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                sheets.Append(
                            new Sheet
                            {
                                Id = workbookPart.GetIdOfPart(worksheetPart),
                                Name = sheetNames[i],
                                SheetId = (uint)i + 1
                            });
            }

            _document.WorkbookPart.Workbook.Save();
        }

        private WorksheetPart GetWorksheetPartByName(string name)
        {
            string id = _document.WorkbookPart.Workbook.Sheets.Descendants<Sheet>().Single(sheet => sheet.Name == name).Id;

            return (WorksheetPart)_document.WorkbookPart.GetPartById(id);
        }

        private void InitializeWriters(string[] sheetNames)
        {
            _worksheetNameToWriterMapping = new Dictionary<string, IWsWriter>();

            foreach (var sheetName in sheetNames)
            {
                var writer = _worksheetWriterFactory.Create(GetWorksheetPartByName(sheetName));

                _worksheetNameToWriterMapping.Add(sheetName, writer);
            }
        }

        private void AddStylePart()
        {
            var stylesPart = _document.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = StylesProvider.ToStylesheet();

            stylesPart.Stylesheet.Save();
        }
        #endregion
    }
}
