using System;
using System.Collections.Generic;
using System.IO;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models;

namespace PrimeDating.BusinessLayer
{
    internal class DailySaverLogService : IDailySaverLogService
    {
        private readonly IDailySaverLogDataService _dailySaverLogDataService;

        private readonly ILogger _logger;

        public DailySaverLogService(IDailySaverLogDataService dailySaverLogDataService, ILogger logger)
        {
            _dailySaverLogDataService = dailySaverLogDataService;

            _logger = logger;
        }

        /// <summary>
        /// Logs the entry.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogEntry(string message)
        {
            _dailySaverLogDataService.LogEntry(message);

            try
            {
                var path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["DailyUploadFilesLog"],
                    "DailyUploadFilesLogs");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                File.WriteAllText(Path.Combine(path, $"{DateTime.Now:yyyyMMdd_hhmmss}.log"), message);
            }
            catch (Exception ex)
            {
                _logger.WarningException($"DailySaverLogService.LogEntry: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="startPeriod">The start period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns></returns>
        public List<DailyDataLogEntry> GetEntries(DateTime startPeriod, DateTime endPeriod)
        {
            return _dailySaverLogDataService.GetEntries(startPeriod, endPeriod);
        }
    }
}
