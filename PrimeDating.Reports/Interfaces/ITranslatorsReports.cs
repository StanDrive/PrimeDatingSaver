using System;
using System.IO;

namespace PrimeDating.Reports.Interfaces
{
    public interface ITranslatorsReports
    {
        /// <summary>
        /// Translatorses the report.
        /// </summary>
        /// <param name="startPeriod">The start period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns></returns>
        Stream TranslatorsReport(DateTime startPeriod, DateTime endPeriod);
    }
}
