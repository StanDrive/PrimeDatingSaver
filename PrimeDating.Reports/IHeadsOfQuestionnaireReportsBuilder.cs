using System.Data;
using System.IO;

namespace PrimeDating.Reports
{
    public interface IHeadsOfQuestionnaireReportsBuilder
    {
        Stream GetGirlsMonthlyReport(DataTable table);

        Stream GetManagersMonthlyReport(DataTable table);
    }
}
