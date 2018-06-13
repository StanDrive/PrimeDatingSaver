using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess
{
    internal class OrdersDataService : IOrdersDataService
    {
        public void AddOrUpdateOrders(List<Orders> orders)
        {
            using (var context = new PrimeDatingContext())
            {
                foreach (var order in orders)
                {
                    context.Orders.AddOrUpdate(order);
                }

                context.SaveChanges();
            }
        }
    }
}
