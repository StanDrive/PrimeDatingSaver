using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess
{
    internal class MenDataService : IMenDataService
    {
        public void AddOrUpdateMen(List<Men> men)
        {
            using (var context = new PrimeDatingContext())
            {
                foreach (var man in men)
                {
                    context.Men.AddOrUpdate(man);
                }

                context.SaveChanges();
            }
        }
    }
}
