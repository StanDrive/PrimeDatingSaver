using System.Collections.Generic;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IOrdersDataService
    {
        void AddOrUpdateOrders(List<Orders> orders);
    }
}
