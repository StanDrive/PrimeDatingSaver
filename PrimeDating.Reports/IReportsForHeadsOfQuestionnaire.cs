using System.IO;

namespace PrimeDating.Reports
{
    interface IReportsForHeadsOfQuestionnaire
    {
        Stream GirlsReport(int year, int month);
    }
}
