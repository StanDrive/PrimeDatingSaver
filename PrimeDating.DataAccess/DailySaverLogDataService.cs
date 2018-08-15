using System;
using System.Collections.Generic;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess
{
    internal class DailySaverLogDataService : IDailySaverLogDataService
    {
        /// <summary>
        /// Logs the entry.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogEntry(string message)
        {
            using (var context = new PrimeDatingContext())
            {
                var entry = new DailyDataSaverLog
                {
                    DateTime = DateTime.Now,
                    Message = message
                };

                context.DailyDataSaverLog.Add(entry);

                context.SaveChanges();
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
            using (var context = new PrimeDatingContext())
            {
                return context.DailyDataSaverLog.Where(t => t.DateTime >= startPeriod && t.DateTime <= endPeriod)
                    .Select(t => new DailyDataLogEntry
                    {
                        Creation = t.DateTime,
                        Message = t.Message
                    }).ToList();
            }
        }
    }
}
