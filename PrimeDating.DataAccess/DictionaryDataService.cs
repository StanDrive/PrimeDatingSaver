using System.Collections.Generic;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;

namespace PrimeDating.DataAccess
{
    internal class DictionaryDataService : IDictionaryDataService
    {
        public Dictionary<int, string> GetManagerRoles()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Roles.ToDictionary(contextRole => contextRole.Id, contextRole => contextRole.Name);
            }
        }

        public Dictionary<int, string> GetGiftStatuses()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.GiftStatuses.ToDictionary(contextRole => contextRole.Id, contextRole => contextRole.Name);
            }
        }

        public Dictionary<int, string> GetPaymentTypes()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.PaymentTypes.ToDictionary(contextRole => contextRole.Id, contextRole => contextRole.Name);
            }
        }
    }
}
