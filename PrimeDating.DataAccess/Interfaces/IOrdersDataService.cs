using System.Collections.Generic;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IOrdersDataService
    {
        void AddOrUpdateOrders(List<Orders> orders);
    }
}
