using System.IO;

namespace PrimeDating.Reports.Interfaces
{
    public interface IReportsForHeadsOfQuestionnaire
    {
        /// <summary>
        /// Girlses the report.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        Stream GirlsReport(int year, int month);
    }
}
