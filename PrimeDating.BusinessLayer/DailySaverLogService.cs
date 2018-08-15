using System;
using System.Collections.Generic;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models;

namespace PrimeDating.BusinessLayer
{
    internal class DailySaverLogService : IDailySaverLogService
    {
        private readonly IDailySaverLogDataService _dailySaverLogDataService;

        public DailySaverLogService(IDailySaverLogDataService dailySaverLogDataService)
        {
            _dailySaverLogDataService = dailySaverLogDataService;
        }

        /// <summary>
        /// Logs the entry.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogEntry(string message)
        {
            _dailySaverLogDataService.LogEntry(message);
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
