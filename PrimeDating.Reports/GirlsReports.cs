using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;
using Payments = PrimeDating.Models.Reports.Payments;

namespace PrimeDating.Reports
{
    internal class GirlsReports : IGirlsReports
    {
        #region private members
        private readonly IReportsData _reportsData;

        private readonly ILogger _logger;

        private readonly List<PaymentTypes> _paymentTypes;
        #endregion

        public GirlsReports(IReportsData reportsData, ILogger logger)
        {
            _reportsData = reportsData;

            _logger = logger;

            _paymentTypes = GetPaymentTypes();
        }

        public Stream GirlsReport(DateTime startPeriod, DateTime endPeriod)
        {
            _logger.Debug($"ReportsFactory.TranslatorsReport [startPeriod: {startPeriod}, endPeriod: {endPeriod}]");

            if (startPeriod >= endPeriod)
            {
                throw new ArgumentException("startPeriod must be lower than endPeriod");
            }

            if (endPeriod - startPeriod > TimeSpan.FromDays(31))
            {
                throw new ArgumentException("Report period can't be bigger than 31 days");
            }

            var reportData = GetGirlsReportData(startPeriod, endPeriod);

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

                var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();

                sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

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
                    Name = reportData.TableName
                };

                // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                sheets.Append(sheet);

                var headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                var columns = new List<string>();

                foreach (DataColumn column in reportData.Columns)
                {
                    columns.Add(column.ColumnName);

                    var cell = new DocumentFormat.OpenXml.Spreadsheet.Cell
                    {
                        DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                        CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName)
                    };

                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dsrow in reportData.Rows)
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

        private DataTable GetGirlsReportData(DateTime dateBegin, DateTime dateEnd)
        {
            var table = GenerateDataTable(dateBegin, dateEnd);

            var payments = _reportsData.GetPaymentsWithinRange(dateBegin, dateEnd);

            foreach (var girl in payments.Select(t => new { t.GirlName, t.GirlId, t.AdminAreaName }).Distinct())
            {
                var managerDataRow = table.NewRow();

                managerDataRow["Name"] = girl.GirlName;

                managerDataRow["Id"] = girl.GirlId;

                managerDataRow["Admin"] = girl.AdminAreaName;

                GetGirlsData(girl.GirlId, girl.AdminAreaName, payments, managerDataRow);

                table.Rows.Add(managerDataRow);
            }

            return table;
        }

        private void GetGirlsData(string girlId, string adminAreaName, List<Payments> payments, DataRow girlDataRow)
        {
            var sum = payments.Where(t => t.GirlId == girlId && t.AdminAreaName == adminAreaName)
                .Sum(payment => payment.Amount);

            girlDataRow["All"] = sum;

            foreach (DataColumn column in girlDataRow.Table.Columns)
            {
                if (!DateTime.TryParse(column.ColumnName, out var result))
                {
                    continue;
                }

                var dateSum = payments.Where(t => t.GirlId == girlId && t.AdminAreaName == adminAreaName && t.Date == result)
                    .Sum(payment => payment.Amount);

                girlDataRow[column.ColumnName] = dateSum;
            }

            foreach (var paymentType in _paymentTypes)
            {
                var paymentTypeSum = payments.Where(t => t.GirlId == girlId && t.AdminAreaName == adminAreaName && t.PaymentType == paymentType.Name)
                    .Sum(payment => payment.Amount);

                girlDataRow[paymentType.Name] = paymentTypeSum;
            }
        }


        private DataTable GenerateDataTable(DateTime dateBegin, DateTime dateEnd)
        {
            var table = new DataTable($"Girls_{dateBegin:dd-MM}_{dateBegin:dd-MM-yyyy}");

            table.Columns.Add(new DataColumn("Name", typeof(string)));
            table.Columns.Add(new DataColumn("Id", typeof(string)));
            table.Columns.Add(new DataColumn("Admin", typeof(string)));
            table.Columns.Add(new DataColumn("All", typeof(decimal)));

            for (var i = 0; i <= (dateEnd - dateBegin).Days; i++)
            {
                table.Columns.Add(new DataColumn(dateBegin.AddDays(i).ToString("dd.MM.yyyy"), typeof(decimal)));
            }

            foreach (var paymentType in _paymentTypes)
            {
                table.Columns.Add(paymentType.Name, typeof(decimal));
            }

            return table;
        }

        private List<PaymentTypes> GetPaymentTypes()
        {
            return _reportsData.GetPaymentTypes().ToList();
        }
    }
}
