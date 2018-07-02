using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;

namespace PrimeDating.Reports
{
    internal class LoggingReports : ILoggingReports
    {
        private readonly IReportsData _reportsData;

        private readonly ILogger _logger;

        private readonly ISpreadsheetBuilder _spreadsheetBuilder;

        public LoggingReports(IReportsData reportsData, ILogger logger, ISpreadsheetBuilder spreadsheetBuilder)
        {
            _reportsData = reportsData;

            _logger = logger;

            _spreadsheetBuilder = spreadsheetBuilder;
        }

        /// <summary>
        /// Gets the todays logs.
        /// </summary>
        /// <returns></returns>
        public Stream GetTodaysLogs()
        {
            _logger.Debug("LoggingReports.GetTodaysLogs");

            var table = GenerateLoggingDataTable($"Logs_{DateTime.Now:dd-MM-yyyy}");

            var data = _reportsData.GetLogsByPeriod(DateTime.Today, DateTime.Today.AddDays(1));

            return BuildDataAndGetStreamReport(table, data);
        }

        /// <summary>
        /// Gets the logs by period.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public Stream GetLogsByPeriod(DateTime startDate, DateTime endDate)
        {
            _logger.Debug($"LoggingReports.GetLogsByPeriod [startDate: {startDate}, endDate: {endDate}]");

            if (startDate > endDate)
            {
                throw new ArgumentException("startDate can't be bigger than endDate");
            }

            var table = GenerateLoggingDataTable($"Logs_{DateTime.Now:dd-MM}-{DateTime.Now:dd-MM-yyyy}");

            var data = _reportsData.GetLogsByPeriod(startDate, endDate);

            return BuildDataAndGetStreamReport(table, data);
        }

        private Stream BuildDataAndGetStreamReport(DataTable table, List<Logging> data)
        {
            foreach (var logs in data)
            {
                var row = table.NewRow();

                row["DateTime"] = logs.DateTime.ToString("dd-MM-yyyy HH:mm:ss");
                row["Level"] = logs.Level;
                row["Message"] = logs.Message;
                row["Exception"] = logs.Exception;

                table.Rows.Add(row);
            }

            return _spreadsheetBuilder.GetSpreadsheetFromDataTable(table);
        }

        private DataTable GenerateLoggingDataTable(string tableName)
        {
            var table = new DataTable(tableName);

            table.Columns.Add(new DataColumn("DateTime"));
            table.Columns.Add(new DataColumn("Level"));
            table.Columns.Add(new DataColumn("Message"));
            table.Columns.Add(new DataColumn("Exception"));

            return table;
        }
    }
}
