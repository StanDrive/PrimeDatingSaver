using System.Data.Entity.Migrations;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess
{
    internal class AdminAreaDataService : IAdminAreaDataService
    {
        public AdminArea GetAdminAreaByName(string adminAreaName)
        {
            using (var context = new PrimeDatingContext())
            {
                return context.AdminAreases.Where(t => t.Name.ToLower() == adminAreaName.ToLower())
                    .Select(t => new AdminArea {Id = t.Id, AdminAreaName = t.Name})
                    .FirstOrDefault();
            }
        }

        public AdminArea CreateAdminArea(string adminAreaName)
        {
            using (var context = new PrimeDatingContext())
            {
                context.AdminAreases.AddOrUpdate(new AdminAreas {Name = adminAreaName});

                context.SaveChanges();

                return context.AdminAreases.Where(t => t.Name == adminAreaName)
                    .Select(t => new AdminArea {Id = t.Id, AdminAreaName = t.Name})
                    .FirstOrDefault();
            }
        }
    }
}
