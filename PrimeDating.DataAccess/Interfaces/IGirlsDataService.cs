using System.Collections.Generic;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IGirlsDataService
    {
        void AddOrUpdateImages(int girlId, List<string> images);

        void AddOrUpdatePassportScans(int girlId, List<string> passportScans);

        void AddOrUpdateGirl(Girls girl);

        void AddOrUpdateManagersGirlsReference(List<ManagersGirls> managersGirlsReference);
    }
}
