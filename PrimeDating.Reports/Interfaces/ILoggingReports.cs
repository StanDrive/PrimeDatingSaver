using System;
using System.IO;

namespace PrimeDating.Reports.Interfaces
{
    public interface ILoggingReports
    {
        /// <summary>
        /// Gets the todays logs.
        /// </summary>
        /// <returns></returns>
        Stream GetTodaysLogs();

        /// <summary>
        /// Gets the logs by period.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        Stream GetLogsByPeriod(DateTime startDate, DateTime endDate);
    }
}
