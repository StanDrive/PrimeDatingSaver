using PrimeDating.Models;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IAdminAreaDataService
    {
        AdminArea GetAdminAreaByName(string adminAreaName);

        AdminArea CreateAdminArea(string adminAreaName);
    }
}
