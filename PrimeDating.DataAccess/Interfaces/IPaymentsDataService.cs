using System.Collections.Generic;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IPaymentsDataService
    {
        void AddOrUpdatePayments(List<Payments> payments);
    }
}
