using System.IO;

namespace PrimeDating.Reports
{
    public interface IReportsForHeadsOfQuestionnaire
    {
        Stream GirlsReport(int year, int month);
    }
}
