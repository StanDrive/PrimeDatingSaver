using System.Data;
using System.IO;

namespace PrimeDating.Reports.Interfaces
{
    public interface IHeadsOfQuestionnaireReportsBuilder
    {
        /// <summary>
        /// Gets the girls monthly report.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        Stream GetGirlsMonthlyReport(DataTable table);

        /// <summary>
        /// Gets the managers monthly report.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        Stream GetManagersMonthlyReport(DataTable table);
    }
}
