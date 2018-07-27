using System.IO;

namespace PrimeDating.Reports.Interfaces
{
    public interface IAdminAreasReports
    {
        Stream GetAdminAreasPercentageStatistics();
    }
}
