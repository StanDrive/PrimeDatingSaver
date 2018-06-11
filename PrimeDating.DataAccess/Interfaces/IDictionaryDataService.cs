using System.Collections.Generic;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IDictionaryDataService
    {
        Dictionary<int, string> GetManagerRoles();

        Dictionary<int, string> GetGiftStatuses();

        Dictionary<int, string> GetPaymentTypes();
    }
}
