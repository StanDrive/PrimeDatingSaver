using System;
using System.IO;

namespace PrimeDating.Reports.Interfaces
{
    public interface IGirlsReports
    {
        /// <summary>
        /// Girlses the report.
        /// </summary>
        /// <param name="startPeriod">The start period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns></returns>
        Stream GirlsReport(DateTime startPeriod, DateTime endPeriod);
    }
}
