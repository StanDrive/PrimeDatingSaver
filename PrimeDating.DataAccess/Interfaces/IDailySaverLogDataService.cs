using System;
using System.Collections.Generic;
using PrimeDating.Models;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IDailySaverLogDataService
    {
        /// <summary>
        /// Logs the entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogEntry(string message);

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <param name="startPeriod">The start period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns></returns>
        List<DailyDataLogEntry> GetEntries(DateTime startPeriod, DateTime endPeriod);
    }
}
