using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Reports;
using PrimeDating.Reports.Interfaces;
using ArgumentException = System.ArgumentException;
using Payments = PrimeDating.Models.Reports.Payments;

namespace PrimeDating.Reports
{
    /// <summary>
    /// ReportsForHeadsOfQuestionnaire
    /// </summary>
    /// <seealso cref="IReportsForHeadsOfQuestionnaire" />
    internal class ReportsForHeadsOfQuestionnaire : IReportsForHeadsOfQuestionnaire
    {
        private readonly IReportsData _reportsData;

        private readonly ILogger _logger;

        private readonly IHeadsOfQuestionnaireReportsBuilder _headsOfQuestionnaireReportsBuilder;

        private readonly List<int> _penaltyPaymentTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsForHeadsOfQuestionnaire"/> class.
        /// </summary>
        /// <param name="reportsData">The reports data.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="headsOfQuestionnaireReportsBuilder">The heads of questionnaire reports builder.</param>
        public ReportsForHeadsOfQuestionnaire(IReportsData reportsData, ILogger logger, IHeadsOfQuestionnaireReportsBuilder headsOfQuestionnaireReportsBuilder)
        {
            _reportsData = reportsData;

            _logger = logger;

            _headsOfQuestionnaireReportsBuilder = headsOfQuestionnaireReportsBuilder;

            _penaltyPaymentTypes = GetPenaltyPaymentTypes();
        }

        /// <summary>
        /// Girlses the report.
        /// </summary>
        /// <param name="startPeriod">The start period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">startPeriod can't be bigger than endPeriod</exception>
        public Stream GirlsReport(DateTime startPeriod, DateTime endPeriod)
        {
            _logger.Debug($"ReportsForHeadsOfQuestionnaire.GirlsReport [StartPeriod: {startPeriod.ToShortDateString()}, EndPeriod: {endPeriod.ToShortDateString()}]");

            if (startPeriod > endPeriod)
            {
                throw new ArgumentException("startPeriod can't be bigger than endPeriod");
            }

            var reportData = GetGirlsReportData(startPeriod, endPeriod);

            return _headsOfQuestionnaireReportsBuilder.GetGirlsMonthlyReport(reportData);
        }

        /// <summary>
        /// Managerses the report.
        /// </summary>
        /// <param name="startPeriod">The start period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">startPeriod can't be bigger than endPeriod</exception>
        public Stream ManagersReport(DateTime startPeriod, DateTime endPeriod)
        {
            _logger.Debug($"ReportsForHeadsOfQuestionnaire.ManagersReport [StartPeriod: {startPeriod.ToShortDateString()}, EndPeriod: {endPeriod.ToShortDateString()}]");

            if (startPeriod > endPeriod)
            {
                throw new ArgumentException("startPeriod can't be bigger than endPeriod");
            }

            var reportData = GetManagersReportData(startPeriod, endPeriod);

            return _headsOfQuestionnaireReportsBuilder.GetManagersMonthlyReport(reportData);
        }

        private DataTable GetManagersReportData(DateTime startDate, DateTime endDate)
        {
            var table = GenerateManagersDataTable(startDate, endDate);

            var payments = _reportsData.GetPaymentsWithinRange(startDate, endDate);

            var gifts = _reportsData.GetGiftsByPeriod(startDate, endDate);

            var managersWithGirls = payments
                .Select(t => new {t.ManagerId, t.GirlId, t.AdminAreaName, t.ManagerFullName}).Distinct()
                .OrderBy(t => t.ManagerId);

            foreach (var managerWithGirl in managersWithGirls)
            {
                var managerWithGirlDataRow = table.NewRow();

                managerWithGirlDataRow["ManagerId"] = managerWithGirl.ManagerId;

                managerWithGirlDataRow["FullName"] = managerWithGirl.ManagerFullName;

                managerWithGirlDataRow["AdminArea"] = managerWithGirl.AdminAreaName;

                managerWithGirlDataRow["GirlId"] = managerWithGirl.GirlId;

                FillManagerWithGirlBalanceInfo(startDate, endDate, managerWithGirl.GirlId,
                    managerWithGirl.AdminAreaName, managerWithGirl.ManagerId, payments, managerWithGirlDataRow);

                FillManagerWithGirlPenaltyInfo(startDate, endDate, managerWithGirl.GirlId,
                    managerWithGirl.AdminAreaName, managerWithGirl.ManagerId, payments, managerWithGirlDataRow);

                FillManagerWithGirlGiftInfo(startDate, endDate, managerWithGirl.GirlId, managerWithGirl.AdminAreaName,
                    managerWithGirl.ManagerId, gifts,
                    managerWithGirlDataRow);

                FillTotalManagerWithGirlAmount(managerWithGirlDataRow);

                table.Rows.Add(managerWithGirlDataRow);
            }

            return table;
        }

        private static void FillTotalManagerWithGirlAmount(DataRow managerWithGirlDataRow)
        {
            managerWithGirlDataRow["TotalMonthAmount"] =
            (from DataColumn column in managerWithGirlDataRow.Table.Columns
                where column.ColumnName.StartsWith("Gifts_") || column.ColumnName.StartsWith("Penalty_") ||
                      column.ColumnName.StartsWith("Balance_")
                select Convert.ToDecimal(managerWithGirlDataRow[column.ColumnName])).Sum();

            managerWithGirlDataRow["TotalPaymentAmount"] = managerWithGirlDataRow["TotalMonthAmount"];
        }

        private static void FillManagerWithGirlGiftInfo(DateTime startDate, DateTime endDate, int girlId,
            string adminArea, int managerId, List<Gift> gifts, DataRow girlDataRow)
        {
            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                girlDataRow[$"Gifts_{startDate.AddDays(i):dd.MM.yyyy}"] =
                    gifts.Where(t =>
                            t.GiftDate.Date == startDate.AddDays(i).Date &&
                            t.GirlId == girlId &&
                            t.ManagerId == managerId &&
                            t.AdminArea == adminArea)
                        .Sum(t => t.Price);
            }
        }

        private void FillManagerWithGirlPenaltyInfo(DateTime startDate, DateTime endDate, int girlId, string adminArea,
            int managerId, List<Payments> payments, DataRow managerWithGirlDataRow)
        {
            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                managerWithGirlDataRow[$"Penalty_{startDate.AddDays(i):dd.MM.yyyy}"] =
                    payments.Where(t =>
                            t.Date == startDate.AddDays(i).Date &&
                            t.GirlId == girlId &&
                            t.ManagerId == managerId &&
                            t.AdminAreaName == adminArea &&
                            _penaltyPaymentTypes.Exists(p => p == t.PaymentType) &&
                            t.Amount < 0)
                        .Sum(t => t.Amount);
            }
        }

        private static void FillManagerWithGirlBalanceInfo(DateTime startDate, DateTime endDate, int girlId,
            string adminArea, int managerId, List<Payments> payments, DataRow managerWithGirlDataRow)
        {
            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                managerWithGirlDataRow[$"Balance_{startDate.AddDays(i):dd.MM.yyyy}"] =
                    payments.Where(t =>
                            t.Date == startDate.AddDays(i).Date &&
                            t.GirlId == girlId &&
                            t.ManagerId == managerId &&
                            t.AdminAreaName == adminArea)
                        .Sum(t => t.Amount);
            }
        }

        private static DataTable GenerateManagersDataTable(DateTime startDate, DateTime endDate)
        {
            var table = new DataTable($"Managers_{startDate:MM-yyyy}");

            table.Columns.Add(new DataColumn("ManagerId", typeof(string)) { Caption = "ID переводчика" });

            table.Columns.Add(new DataColumn("FullName", typeof(string)) { Caption = "Ф.И.О." });

            table.Columns.Add(new DataColumn("AdminArea", typeof(string)) { Caption = "Админка" });

            table.Columns.Add(new DataColumn("GirlId", typeof(string)) { Caption = "ID анкеты" });

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(
                    new DataColumn($"Balance_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal))
                    {
                        Caption = $"{startDate.AddDays(i):dd.MM.yyyy}"
                    });
            }

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Penalty_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)) { Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Gifts_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)) { Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            table.Columns.Add(new DataColumn("TotalMonthAmount", typeof(decimal)) { Caption = "Баланс по переводчику за месяц" });

            table.Columns.Add(new DataColumn("MeetingsAmount", typeof(decimal)) { Caption = "Баланс по встречам" });

            table.Columns.Add(new DataColumn("TotalPaymentAmount", typeof(decimal)) { Caption = "Итоговая сумма к выплате" });

            table.Columns.Add(new DataColumn("Payed", typeof(decimal)) { Caption = "Выплачено" });

            table.Columns.Add(new DataColumn("Debt", typeof(decimal)) { Caption = "Долг" });

            return table;
        }

        #region private
        private DataTable GetGirlsReportData(DateTime startDate, DateTime endDate)
        {
            var table = GenerateGirlsDataTable(startDate, endDate);

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

                FillTotalGirlsGiftAmount(startDate, endDate, girlDataRow);

                table.Rows.Add(girlDataRow);
            }

            return table;
        }

        private static void FillTotalGirlsGiftAmount(DateTime startDate, DateTime endDate, DataRow girlDataRow)
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

        private static void FillGirlGiftInfo(DateTime startDate, DateTime endDate, int girlId, string adminArea, int assignedManagerId, List<Gift> gifts, DataRow girlDataRow)
        {
            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                girlDataRow[$"Gifts_{startDate.AddDays(i):dd.MM.yyyy}"] =
                    gifts.Where(t =>
                            t.GiftDate.Date == startDate.AddDays(i).Date &&
                            t.GirlId == girlId &&
                            t.ManagerId == assignedManagerId &&
                            t.AdminArea == adminArea)
                        .Sum(t => t.Price);
            }
        }

        private static void FillGirlBalanceInfo(DateTime startDate, DateTime endDate, int girlId, string adminArea, int assignedManagerId, List<Payments> payments, DataRow girlDataRow)
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

        private static DataTable GenerateGirlsDataTable(DateTime startDate, DateTime endDate)
        {
            var table = new DataTable($"Girls_{startDate:MM-yyyy}");

            table.Columns.Add(new DataColumn("GirlId", typeof(string)) { Caption = "ID девушки" });

            table.Columns.Add(new DataColumn("FullName", typeof(string)) { Caption = "Ф.И.О." });

            table.Columns.Add(new DataColumn("Manager", typeof(string)) { Caption = "Переводчик" });

            table.Columns.Add(new DataColumn("AdminArea", typeof(string)) { Caption = "Админка" });

            table.Columns.Add(new DataColumn("Site", typeof(string)) { Caption = "Сайт" });

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Balance_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)) { Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Penalty_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)) { Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            for (var i = 0; i <= (endDate - startDate).Days; i++)
            {
                table.Columns.Add(new DataColumn($"Gifts_{startDate.AddDays(i):dd.MM.yyyy}", typeof(decimal)) { Caption = $"{startDate.AddDays(i):dd.MM.yyyy}" });
            }

            table.Columns.Add(new DataColumn("TotalMonthAmount", typeof(decimal)) { Caption = "Общая сумма по балансам за месяц" });

            table.Columns.Add(new DataColumn("TotalPenaltiesAmount", typeof(decimal)) { Caption = "Общая сумма штрафов, за месяц" });

            table.Columns.Add(new DataColumn("TotalGiftAmountToClient", typeof(decimal)) { Caption = "Сумма подарков, которая озвучена клиентке" });

            table.Columns.Add(new DataColumn("TotalGiftAmount", typeof(decimal)) { Caption = "Общая сумма по подаркам в месяц" });

            table.Columns.Add(new DataColumn("FormPercentOfPayment", typeof(decimal)) { Caption = "Процент выплаты за анкету" });

            table.Columns.Add(new DataColumn("GiftPercentOfPayment", typeof(decimal)) { Caption = "Процент выплаты за подарок" });

            table.Columns.Add(new DataColumn("TotalPaymentAmount", typeof(decimal)) { Caption = "Итоговая сумма к выплате" });

            table.Columns.Add(new DataColumn("AlreadyPayed", typeof(decimal)) { Caption = "Выплачено" });

            table.Columns.Add(new DataColumn("Debt", typeof(decimal)) { Caption = "Долг" });

            return table;
        }

        private List<int> GetPenaltyPaymentTypes()
        {
            return _reportsData.GetPaymentTypes().Where(t => t.Penalty == 1).Select(t => t.Id).ToList();
        }
        #endregion
    }
}

