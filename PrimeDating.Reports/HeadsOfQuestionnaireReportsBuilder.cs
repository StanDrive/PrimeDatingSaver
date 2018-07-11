using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PrimeDating.Reports
{
    internal class HeadsOfQuestionnaireReportsBuilder : IHeadsOfQuestionnaireReportsBuilder
    {
        private const int TotalSumColumnsCount = 9;

        private const int HeaderHeightSize = 60;

        public Stream GetGirlsMonthlyReport(DataTable table)
        {
            var memoryStream = new MemoryStream();

            using (var objSpreadsheet =
                SpreadsheetDocument.Create(memoryStream, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                objSpreadsheet.AddWorkbookPart();

                AddStyleSheet(objSpreadsheet);

                objSpreadsheet.WorkbookPart.Workbook =
                    new DocumentFormat.OpenXml.Spreadsheet.Workbook
                    {
                        Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets()
                    };

                uint sheetId = 1;

                var sheetPart = objSpreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();

                sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();

                sheetPart.Worksheet.AppendChild(GetSpreadsheetColumns(table));

                sheetPart.Worksheet.AppendChild(sheetData);

                sheetPart.Worksheet.InsertAfter(GetMergeCells(table),
                    sheetPart.Worksheet.Elements<SheetData>().First());

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

                FillFirstHeaderRow(headerRow, table);

                sheetData.AppendChild(headerRow);

                headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row
                {
                    Height = HeaderHeightSize,
                    CustomHeight = true,
                    StyleIndex = 1
                };

                foreach (DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);

                    var cell = new DocumentFormat.OpenXml.Spreadsheet.Cell
                    {
                        DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                        CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(string.IsNullOrWhiteSpace(column.Caption) ? column.ColumnName : column.Caption),
                        StyleIndex = 1
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

        private WorkbookStylesPart AddStyleSheet(SpreadsheetDocument spreadsheet)
        {
            var stylesheet = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>();

            var workbookstylesheet = new Stylesheet();

            var defaultFont = new Font();

            var allFonts = new Fonts();

            allFonts.Append(defaultFont);

            var defaultFill = new Fill();

            var allFills = new Fills();

            allFills.Append(defaultFill);

            var defaultBorder = new Border();

            var allBorders = new Borders();

            allBorders.Append(defaultBorder);

            var defaultCellformat = new CellFormat { FontId = 0, FillId = 0, BorderId = 0 };

            var cellformatWithWrapText = new CellFormat(new Alignment { WrapText = true, Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });

            var allCellformats = new CellFormats();

            allCellformats.Append(defaultCellformat);

            allCellformats.Append(cellformatWithWrapText);

            workbookstylesheet.Append(allFonts);

            workbookstylesheet.Append(allFills);

            workbookstylesheet.Append(allBorders);

            workbookstylesheet.Append(allCellformats);

            stylesheet.Stylesheet = workbookstylesheet;

            stylesheet.Stylesheet.Save();

            return stylesheet;
        }

        private void FillFirstHeaderRow(Row headerRow, DataTable table)
        {
            headerRow.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Cell
            {
                DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(table.Columns["GirlId"].Caption),
                StyleIndex = 1
            });

            headerRow.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Cell
            {
                DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(table.Columns["FullName"].Caption),
                StyleIndex = 1
            });

            headerRow.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Cell
            {
                DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(table.Columns["Manager"].Caption),
                StyleIndex = 1
            });

            headerRow.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Cell
            {
                DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(table.Columns["AdminArea"].Caption),
                StyleIndex = 1
            });

            headerRow.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Cell
            {
                DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(table.Columns["Site"].Caption),
                StyleIndex = 1
            });

            for (var i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].ColumnName.StartsWith("Balance_"))
                {
                    headerRow.AppendChild(new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("Балансы"),
                        StyleIndex = 1
                    });
                }

                if (table.Columns[i].ColumnName.StartsWith("Penalty_"))
                {
                    headerRow.AppendChild(new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("Штрафы"),
                        StyleIndex = 1
                    });
                }

                if (table.Columns[i].ColumnName.StartsWith("Gifts_"))
                {
                    headerRow.AppendChild(new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("Подарки"),
                        StyleIndex = 1
                    });
                }
            }

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["TotalMonthAmount"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["TotalPenaltiesAmount"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["TotalGiftAmountToClient"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["TotalGiftAmount"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["FormPercentOfPayment"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["GiftPercentOfPayment"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["TotalPaymentAmount"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["AlreadyPayed"].Caption), StyleIndex = 1 });

            headerRow.AppendChild(new Cell { DataType = CellValues.String, CellValue = new CellValue(table.Columns["Debt"].Caption), StyleIndex = 1 });
        }

        private MergeCells GetMergeCells(DataTable table)
        {
            var mergeCellsList = new List<MergeCell>
            {
                new MergeCell {Reference = new StringValue("A1:A2")},
                new MergeCell {Reference = new StringValue("B1:B2")},
                new MergeCell {Reference = new StringValue("C1:C2")},
                new MergeCell {Reference = new StringValue("D1:D2")},
                new MergeCell {Reference = new StringValue("E1:E2")}
            };

            var columnWithDays = table.Columns.Cast<DataColumn>().Count(column => column.ColumnName.StartsWith("Balance_"));

            var lastOperatedColumnNumber = 6;

            mergeCellsList.Add(new MergeCell
            {
                Reference = new StringValue(
                    $"{GetExcelColumnLetter(lastOperatedColumnNumber)}1:{GetExcelColumnLetter(lastOperatedColumnNumber + columnWithDays - 1)}1")
            });

            lastOperatedColumnNumber += columnWithDays;

            mergeCellsList.Add(new MergeCell
            {
                Reference = new StringValue(
                    $"{GetExcelColumnLetter(lastOperatedColumnNumber)}1:{GetExcelColumnLetter(lastOperatedColumnNumber + columnWithDays - 1)}1")
            });

            lastOperatedColumnNumber += columnWithDays;

            mergeCellsList.Add(new MergeCell
            {
                Reference = new StringValue(
                    $"{GetExcelColumnLetter(lastOperatedColumnNumber)}1:{GetExcelColumnLetter(lastOperatedColumnNumber + columnWithDays - 1)}1")
            });

            lastOperatedColumnNumber += columnWithDays;

            for (var i = 0; i < TotalSumColumnsCount; i++)
            {
                mergeCellsList.Add(new MergeCell
                {
                    Reference = new StringValue(
                        $"{GetExcelColumnLetter(lastOperatedColumnNumber)}1:{GetExcelColumnLetter(lastOperatedColumnNumber++)}2")
                });
            }

            return new MergeCells(mergeCellsList);
        }

        private DocumentFormat.OpenXml.Spreadsheet.Columns GetSpreadsheetColumns(DataTable table)
        {
            uint columnCounter = 1;

            var columnsList = new List<Column>
            {
                new Column {Min = columnCounter, Max = columnCounter++, Width = 13, CustomWidth = true},
                new Column {Min = columnCounter, Max = columnCounter++, Width = 15, CustomWidth = true},
                new Column {Min = columnCounter, Max = columnCounter++, Width = 14, CustomWidth = true},
                new Column {Min = columnCounter, Max = columnCounter++, Width = 14, CustomWidth = true},
                new Column {Min = columnCounter, Max = columnCounter++, Width = 14, CustomWidth = true}
            };

            columnsList.AddRange(from DataColumn column in table.Columns
                                 where column.ColumnName.StartsWith("Balance_") ||
                                       column.ColumnName.StartsWith("Penalty_") ||
                                       column.ColumnName.StartsWith("Gifts_")
                                 select new Column { Min = columnCounter, Max = columnCounter++, Width = 10, CustomWidth = true });

            for (var i = 0; i < TotalSumColumnsCount; i++)
            {
                columnsList.Add(new Column { Min = columnCounter, Max = columnCounter++, Width = 14, CustomWidth = true });
            }

            return new DocumentFormat.OpenXml.Spreadsheet.Columns(columnsList);
        }

        public Stream GetManagersMonthlyReport(DataTable table)
        {
            throw new NotImplementedException();
        }

        private string GetExcelColumnLetter(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;

            while (dividend > 0)
            {
                var module = (dividend - 1) % 26;

                columnName = Convert.ToChar(65 + module) + columnName;

                dividend = (dividend - module) / 26;
            }

            return columnName;
        }
    }
}
