using System.Data.Entity.Migrations;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess
{
    internal class MenDataService : IMenDataService
    {
        public void AddOrUpdateMan(Men man)
        {
            using (var context = new PrimeDatingContext())
            {
                context.Men.AddOrUpdate(man);

                context.SaveChanges();
            }
        }
    }
}
