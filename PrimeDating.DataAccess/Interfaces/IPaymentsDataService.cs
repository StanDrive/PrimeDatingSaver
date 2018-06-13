using System.Collections.Generic;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IPaymentsDataService
    {
        void AddOrUpdatePayments(List<Payments> payments);
    }
}
