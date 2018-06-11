using System.Data.Entity.Migrations;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess
{
    internal class ManagerDataService : IManagerDataService
    {
        public void AddOrUpdateManager(Managers manager)
        {
            using (var context = new PrimeDatingContext())
            {
                context.Managers.AddOrUpdate(manager);

                context.SaveChanges();
            }
        }
    }
}
