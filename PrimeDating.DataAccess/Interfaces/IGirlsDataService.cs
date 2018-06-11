using System.Collections.Generic;
using PrimeDating.DataAccess.Models;

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
