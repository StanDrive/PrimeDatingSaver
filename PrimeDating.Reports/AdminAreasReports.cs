using System;
using System.Data;
using System.IO;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Reports.Interfaces;

namespace PrimeDating.Reports
{
    internal class AdminAreasReports : IAdminAreasReports
    {
        #region private members
        private readonly IReportsData _reportsData;

        private readonly ILogger _logger;

        private readonly ISpreadsheetBuilder _spreadsheetBuilder;
        #endregion

        public AdminAreasReports(IReportsData reportsData, ILogger logger, ISpreadsheetBuilder spreadsheetBuilder)
        {
            _reportsData = reportsData;

            _logger = logger;

            _spreadsheetBuilder = spreadsheetBuilder;
        }

        /// <summary>
        /// Gets the admin areas percentage statistics.
        /// </summary>
        /// <returns></returns>
        public Stream GetAdminAreasPercentageStatistics()
        {
            _logger.Debug($"AdminAreasReports.GetAdminAreasPercentageStatistics");

            var reportData = GetAdminAreasPercentageStatisticsData();

            return _spreadsheetBuilder.GetSpreadsheetFromDataTable(reportData);
        }

        private DataTable GetAdminAreasPercentageStatisticsData()
        {
            var table = GenerateDataTable();

            var adminData = _reportsData.GetAdminAreasStatistics();

            foreach (var adminAreaStatistic in adminData)
            {
                var row = table.NewRow();

                row["AdminArea"] = adminAreaStatistic.AdminAreaName;
                row["PaymentsSum"] = adminAreaStatistic.PaymentsSum;
                row["Percentage"] = Math.Round(adminAreaStatistic.Percentage, 2);

                table.Rows.Add(row);
            }

            return table;
        }

        private DataTable GenerateDataTable()
        {
            var table = new DataTable("AdminAreasPercentageStatistics");

            table.Columns.Add(new DataColumn("AdminArea", typeof(string)){Caption = "ID админки" });
            table.Columns.Add(new DataColumn("PaymentsSum", typeof(string)){Caption = "Итоговая сумма к выплате по всем переводчикам на анкете" });
            table.Columns.Add(new DataColumn("Percentage", typeof(string)){Caption = "Процент по данному админу" });
            table.Columns.Add(new DataColumn("Payed", typeof(decimal)) {Caption = "Выплачено"});
            table.Columns.Add(new DataColumn("Debt", typeof(decimal)) {Caption = "Долг"});

            return table;
        }
    }
}
