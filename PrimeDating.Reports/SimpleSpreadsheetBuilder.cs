using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PrimeDating.Reports.Interfaces;

// ReSharper disable PossiblyMistakenUseOfParamsMethod

namespace PrimeDating.Reports
{
    internal class SimpleSpreadsheetBuilder : ISpreadsheetBuilder
    {
        public Stream GetSpreadsheetFromDataTable(DataTable table)
        {
            var memoryStream = new MemoryStream();

            using (var objSpreadsheet =
                SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                objSpreadsheet.AddWorkbookPart();

                objSpreadsheet.WorkbookPart.Workbook =
                    new Workbook
                    {
                        Sheets = new Sheets()
                    };

                uint sheetId = 1;

                var sheetPart = objSpreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();

                sheetPart.Worksheet = new Worksheet();

                var sheetData = new SheetData();

                sheetPart.Worksheet.AppendChild(sheetData);

                var sheets = objSpreadsheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();

                var relationshipId = objSpreadsheet.WorkbookPart.GetIdOfPart(sheetPart);

                if (sheets.Elements<Sheet>().Any())
                {
                    sheetId =
                        sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                var sheet = new Sheet
                {
                    Id = relationshipId,
                    SheetId = sheetId,
                    Name = table.TableName
                };

                sheets.Append(sheet);

                var headerRow = new Row();

                var columns = new List<string>();

                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(string.IsNullOrWhiteSpace(column.Caption) ? column.ColumnName : column.Caption)
                    };

                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    var newRow = new Row();

                    foreach (var col in columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(dsrow[col].ToString())
                        };

                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                return memoryStream;
            }
        }
    }
}
