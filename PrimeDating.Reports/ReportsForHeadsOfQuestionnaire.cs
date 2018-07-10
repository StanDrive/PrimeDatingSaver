using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Reports;
using ArgumentException = System.ArgumentException;
using Payments = PrimeDating.Models.Reports.Payments;

namespace PrimeDating.Reports
{
    internal class ReportsForHeadsOfQuestionnaire : IReportsForHeadsOfQuestionnaire
    {
        private readonly IReportsData _reportsData;

        private readonly ILogger _logger;

        private readonly ISpreadsheetBuilder _spreadsheetBuilder;

        private readonly List<int> _penaltyPaymentTypes;

        public ReportsForHeadsOfQuestionnaire(IReportsData reportsData, ILogger logger, ISpreadsheetBuilder spreadsheetBuilder)
        {
            _reportsData = reportsData;

            _logger = logger;

            _spreadsheetBuilder = spreadsheetBuilder;

            _penaltyPaymentTypes = GetPenaltyPaymentTypes();
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

            var gifts = _reportsData.GetGiftsByPeriod(startDate, endDate);

            var girls = _reportsData.GetAllGirls();

            foreach (var girl in girls)
            {
                var girlDataRow = table.NewRow();

                girlDataRow["GirlId"] = girl.GirlId;

                girlDataRow["FullName"] = girl.FullName;

                girlDataRow["Manager"] = girl.AssignedManager;

                girlDataRow["AdminArea"] = girl.AdminArea;

                FillGirlBalanceInfo(startDate, endDate, girl.GirlId, girl.AdminArea, girl.AssignedManagerId, payments, girlDataRow);

                FillGirlPenaltyInfo(startDate, endDate, girl.GirlId, girl.AdminArea, girl.AssignedManagerId, payments, girlDataRow);

                FillGirlGiftInfo(startDate, endDate, girl.GirlId, girl.AdminArea, girl.AssignedManagerId, gifts,
                    girlDataRow);

                FillTotalGiftAmount(startDate, endDate, girlDataRow);

                table.Rows.Add(girlDataRow);
            }

            return table;
        }

        private void FillTotalGiftAmount(DateTime startDate, DateTime endDate, DataRow girlDataRow)
        {
            decimal totalGiftAmount = 0;

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                totalGiftAmount += Convert.ToDecimal(girlDataRow[$"Gifts_{startDate.AddDays(i):dd.MM.yyyy}"]);

                totalGiftAmount += Convert.ToDecimal(girlDataRow[$"Balance_{startDate.AddDays(i):dd.MM.yyyy}"]);

                totalGiftAmount += Convert.ToDecimal(girlDataRow[$"Penalty_{startDate.AddDays(i):dd.MM.yyyy}"]);
            }

            girlDataRow["TotalGiftAmount"] = totalGiftAmount;
        }

        private void FillGirlPenaltyInfo(DateTime startDate, DateTime endDate, int girlId, string adminArea, int assignedManagerId, List<Payments> payments, DataRow girlDataRow)
        {
            decimal monthPenaltyAmount = 0;

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                girlDataRow[$"Penalty_{startDate.AddDays(i):dd.MM.yyyy}"] =
                    payments.Where(t =>
                            t.Date == startDate.AddDays(i).Date &&
                            t.GirlId == girlId &&
                            t.ManagerId == assignedManagerId &&
                            t.AdminAreaName == adminArea &&
                            _penaltyPaymentTypes.Exists(p => p == t.PaymentType) &&
                            t.Amount < 0)
                        .Sum(t => t.Amount);

                monthPenaltyAmount += Convert.ToDecimal(girlDataRow[$"Penalty_{startDate.AddDays(i):dd.MM.yyyy}"]);
            }

            girlDataRow["TotalPenaltiesAmount"] = monthPenaltyAmount;
        }

        private void FillGirlGiftInfo(DateTime startDate, DateTime endDate, int girlId, string adminArea, int assignedManagerId, List<Gift> gifts, DataRow girlDataRow)
        {
            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                girlDataRow[$"Gifts_{startDate.AddDays(i):dd.MM.yyyy}"] =
                    gifts.Where(t =>
                            t.GiftDate == startDate.AddDays(i).Date &&
                            t.GirlId == girlId &&
                            t.ManagerId == assignedManagerId &&
                            t.AdminArea == adminArea)
                        .Sum(t => t.Price);
            }
        }

        private void FillGirlBalanceInfo(DateTime startDate, DateTime endDate, int girlId, string adminArea, int assignedManagerId, List<Payments> payments, DataRow girlDataRow)
        {
            decimal monthBalanceAmount = 0;

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                girlDataRow[$"Balance_{startDate.AddDays(i):dd.MM.yyyy}"] =
                    payments.Where(t =>
                            t.Date == startDate.AddDays(i).Date &&
                            t.GirlId == girlId &&
                            t.ManagerId == assignedManagerId &&
                            t.AdminAreaName == adminArea)
                        .Sum(t => t.Amount);

                monthBalanceAmount += Convert.ToDecimal(girlDataRow[$"Balance_{startDate.AddDays(i):dd.MM.yyyy}"]);
            }

            girlDataRow["TotalMonthAmount"] = monthBalanceAmount;
        }

        private static DataTable GenerateDataTable(DateTime startDate, DateTime endDate)
        {
            var table = new DataTable($"Girls_{startDate:MM-yyyy}");

            table.Columns.Add(new DataColumn("GirlId", typeof(string)){Caption = "ID девушки" });

            table.Columns.Add(new DataColumn("FullName", typeof(string)){Caption = "Ф.И.О." });

            table.Columns.Add(new DataColumn("Manager", typeof(string)){Caption = "Переводчик" });

            table.Columns.Add(new DataColumn("AdminArea", typeof(string)){Caption = "Админка"});

            table.Columns.Add(new DataColumn("Site", typeof(string)) {Caption = "Сайт"});

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Balance_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)){Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Penalty_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)) { Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Gifts_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)) { Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            table.Columns.Add(new DataColumn("TotalMonthAmount", typeof(decimal)){Caption = "Общая сумма по балансам за месяц" });

            table.Columns.Add(new DataColumn("TotalPenaltiesAmount", typeof(decimal)){Caption = "Общая сумма штрафов, за месяц" });

            table.Columns.Add(new DataColumn("TotalGiftAmountToClient", typeof(decimal)){Caption = "Сумма подарков, которая озвучена клиентке" });

            table.Columns.Add(new DataColumn("TotalGiftAmount", typeof(decimal)){Caption = "Общая сумма по подаркам в месяц" });

            table.Columns.Add(new DataColumn("FormPercentOfPayment", typeof(decimal)){Caption = "Процент выплаты за анкету" });

            table.Columns.Add(new DataColumn("GiftPercentOfPayment", typeof(decimal)){Caption = "Процент выплаты за подарок" });

            table.Columns.Add(new DataColumn("TotalPaymentAmount", typeof(decimal)){Caption = "Итоговая сумма к выплате" });

            table.Columns.Add(new DataColumn("AlreadyPayed", typeof(decimal)){Caption = "Выплачено"});

            table.Columns.Add(new DataColumn("Debt", typeof(decimal)){Caption = "Долг"});

            return table;
        }

        private List<int> GetPenaltyPaymentTypes()
        {
            return _reportsData.GetPaymentTypes().Where(t => t.Penalty == 1).Select(t => t.Id).ToList();
        }
    }
}
