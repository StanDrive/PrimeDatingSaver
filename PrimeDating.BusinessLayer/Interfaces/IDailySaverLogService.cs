using System;
using System.Collections.Generic;
using PrimeDating.Models;

namespace PrimeDating.BusinessLayer.Interfaces
{
    public interface IDailySaverLogService
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
