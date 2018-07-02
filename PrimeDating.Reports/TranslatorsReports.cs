using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;
using DataTable = System.Data.DataTable;
using Payments = PrimeDating.Models.Reports.Payments;

namespace PrimeDating.Reports
{
    internal class TranslatorsReports : ITranslatorsReports
    {
        #region private members
        private readonly IReportsData _reportsData;

        private readonly ILogger _logger;

        private readonly ISpreadsheetBuilder _spreadsheetBuilder;

        private readonly List<PaymentTypes> _paymentTypes;
        #endregion

        public TranslatorsReports(IReportsData reportsData, ILogger logger, ISpreadsheetBuilder spreadsheetBuilder)
        {
            _reportsData = reportsData;

            _logger = logger;

            _spreadsheetBuilder = spreadsheetBuilder;

            _paymentTypes = GetPaymentTypes();
        }

        public Stream TranslatorsReport(DateTime startPeriod, DateTime endPeriod)
        {
            _logger.Debug($"TranslatorsReports.TranslatorsReport [startPeriod: {startPeriod}, endPeriod: {endPeriod}]");

            if (startPeriod > endPeriod)
            {
                throw new ArgumentException("startPeriod can't be bigger than endPeriod");
            }

            if (endPeriod - startPeriod > TimeSpan.FromDays(31))
            {
                throw new ArgumentException("Report period can't be bigger than 31 days");
            }

            var reportData = GetTranslatorsReportData(startPeriod, endPeriod);

            return _spreadsheetBuilder.GetSpreadsheetFromDataTable(reportData);
        }

        #region private members
        private DataTable GetTranslatorsReportData(DateTime dateBegin, DateTime dateEnd)
        {
            var table = GenerateDataTable(dateBegin, dateEnd);

            var payments = _reportsData.GetPaymentsWithinRange(dateBegin, dateEnd);

            foreach (var manager in payments.Select(t => new { t.ManagerName, t.ManagerId, t.AdminAreaName }).Distinct())
            {
                var managerDataRow = table.NewRow();

                managerDataRow["Name"] = manager.ManagerName;

                managerDataRow["Id"] = manager.ManagerId;

                managerDataRow["Admin"] = manager.AdminAreaName;

                GetManagerData(manager.ManagerId, manager.AdminAreaName, payments, managerDataRow);

                table.Rows.Add(managerDataRow);
            }

            return table;
        }

        private void GetManagerData(string managerId, string adminAreaName, List<Payments> payments, DataRow managerDataRow)
        {
            var sum = payments.Where(t => t.ManagerId == managerId && t.AdminAreaName == adminAreaName)
                .Sum(payment => payment.Amount);

            managerDataRow["All"] = sum;

            foreach (DataColumn column in managerDataRow.Table.Columns)
            {
                if (!DateTime.TryParse(column.ColumnName, out var result))
                {
                    continue;
                }

                var dateSum = payments.Where(t => t.ManagerId == managerId && t.AdminAreaName == adminAreaName && t.Date == result)
                    .Sum(payment => payment.Amount);

                managerDataRow[column.ColumnName] = dateSum;
            }

            foreach (var paymentType in _paymentTypes)
            {
                var paymentTypeSum = payments.Where(t => t.ManagerId == managerId && t.AdminAreaName == adminAreaName && t.PaymentType == paymentType.Name)
                    .Sum(payment => payment.Amount);

                managerDataRow[paymentType.Name] = paymentTypeSum;
            }
        }


        private DataTable GenerateDataTable(DateTime dateBegin, DateTime dateEnd)
        {
            var table = new DataTable($"Translators_{dateBegin:dd-MM}_{dateEnd:dd-MM-yyyy}");

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
        #endregion
    }
}
