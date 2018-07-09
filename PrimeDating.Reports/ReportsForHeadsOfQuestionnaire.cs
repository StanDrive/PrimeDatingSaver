using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using ArgumentException = System.ArgumentException;

namespace PrimeDating.Reports
{
    internal class ReportsForHeadsOfQuestionnaire : IReportsForHeadsOfQuestionnaire
    {
        private readonly IReportsData _reportsData;

        private readonly ILogger _logger;

        private readonly ISpreadsheetBuilder _spreadsheetBuilder;

        public ReportsForHeadsOfQuestionnaire(IReportsData reportsData, ILogger logger, ISpreadsheetBuilder spreadsheetBuilder)
        {
            _reportsData = reportsData;

            _logger = logger;

            _spreadsheetBuilder = spreadsheetBuilder;
        }

        public Stream GirlsReport(int year, int month)
        {
            _logger.Debug($"ReportsForHeadsOfQuestionnaire.GirlsReport [year: {year}, month: {month}]");

            if (year < 1900 || year > 2100)
            {
                throw new ArgumentException("Year can't be lower than 1900 or bigger then 2100");
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("Month can't be lower than 1 or bigger then 12");
            }

            var startDate = new DateTime(year, month, 1);

            var endDate = startDate.AddMonths(1).AddDays(-1);

            var reportData = GetGirlsReportData(startDate, endDate);

            return _spreadsheetBuilder.GetSpreadsheetFromDataTable(reportData);
        }

        private DataTable GetGirlsReportData(DateTime startDate, DateTime endDate)
        {
            var table = GenerateDataTable(startDate, endDate);

            var payments = _reportsData.GetPaymentsWithinRange(startDate, endDate);

            var girls = _reportsData.GetAllGirls();

            foreach (var girl in girls)
            {
                var managerDataRow = table.NewRow();

                managerDataRow["ID девушки"] = girl.GirlId;

                managerDataRow["Ф.И.О."] = girl.FullName;

                managerDataRow["Переводчик"] = girl.AssignedManager;

                managerDataRow["Админка"] = girl.AdminArea;

                GetGirlsData(girl.GirlId, girl.AdminArea, payments, managerDataRow);

                table.Rows.Add(managerDataRow);
            }
        }

        private static DataTable GenerateDataTable(DateTime startDate, DateTime endDate)
        {
            var table = new DataTable($"Girls_{startDate:dd-MM-yyyy}");

            table.Columns.Add(new DataColumn("ID девушки", typeof(string)));

            table.Columns.Add(new DataColumn("Ф.И.О.", typeof(string)));

            table.Columns.Add(new DataColumn("Переводчик", typeof(string)));

            table.Columns.Add(new DataColumn("Админка", typeof(string)));

            table.Columns.Add(new DataColumn("Сайт", typeof(string)));

            for (var i = 1; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"balance_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)));
            }

            for (var i = 1; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"penalty_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)));
            }

            for (var i = 1; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"gifts_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)));
            }

            table.Columns.Add(new DataColumn("Общая сумма по балансам за месяц", typeof(decimal)));

            table.Columns.Add(new DataColumn("Общая сумма штрафов, за месяц", typeof(decimal)));

            table.Columns.Add(new DataColumn("Сумма подарков, которая озвучена клиентке", typeof(decimal)));

            table.Columns.Add(new DataColumn("Общая сумма по подаркам в месяц", typeof(decimal)));

            table.Columns.Add(new DataColumn("Процент выплаты за анкету", typeof(decimal)));

            table.Columns.Add(new DataColumn("Процент выплаты за подарок", typeof(decimal)));

            table.Columns.Add(new DataColumn("Итоговая сумма к выплате", typeof(decimal)));

            table.Columns.Add(new DataColumn("Выплачено", typeof(decimal)));

            table.Columns.Add(new DataColumn("Долг", typeof(decimal)));

            return table;
        }
    }
}
