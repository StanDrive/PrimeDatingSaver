using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PrimeDating.Reports
{
    internal class SpreadsheetBuilder : ISpreadsheetBuilder
    {
        public Stream GetSpreadsheetFromDataTable(DataTable table)
        {
            var memoryStream = new MemoryStream();

            using (var objSpreadsheet =
                SpreadsheetDocument.Create(memoryStream, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                objSpreadsheet.AddWorkbookPart();

                objSpreadsheet.WorkbookPart.Workbook =
                    new DocumentFormat.OpenXml.Spreadsheet.Workbook
                    {
                        Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets()
                    };

                uint sheetId = 1;

                var sheetPart = objSpreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();

                sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();

                //var spreadsheetcolumns = new DocumentFormat.OpenXml.Spreadsheet.Columns();

                //var column1 = new Column() { Min = 1, Max = 2, Width = 30, CustomWidth = true };

                //var column2 = new Column() { Min = 3, Max = 3, Width = 20, CustomWidth = true };

                //spreadsheetcolumns.Append(column1);
                //spreadsheetcolumns.Append(column2);

                //sheetPart.Worksheet.AppendChild(spreadsheetcolumns);

                sheetPart.Worksheet.AppendChild(sheetData);

                var sheets = objSpreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();

                var relationshipId = objSpreadsheet.WorkbookPart.GetIdOfPart(sheetPart);

                if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Any())
                {
                    sheetId =
                        sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }

                var sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet
                {
                    Id = relationshipId,
                    SheetId = sheetId,
                    Name = table.TableName
                };

                // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                sheets.Append(sheet);

                var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                var columns = new List<string>();

                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    var cell = new DocumentFormat.OpenXml.Spreadsheet.Cell
                    {
                        DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                        CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(string.IsNullOrWhiteSpace(column.Caption) ? column.ColumnName : column.Caption)
                    };

                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in table.Rows)
                {
                    var newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    foreach (var col in columns)
                    {
                        var cell = new DocumentFormat.OpenXml.Spreadsheet.Cell
                        {
                            DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                            CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString())
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
