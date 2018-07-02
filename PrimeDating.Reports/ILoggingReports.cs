using System;
using System.IO;

namespace PrimeDating.Reports
{
    public interface ILoggingReports
    {
        Stream GetTodaysLogs();

        Stream GetLogsByPeriod(DateTime startDate, DateTime endDate);
    }
}
