using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess
{
    internal class GirlsDataService : IGirlsDataService
    {
        public void AddOrUpdateImages(int girlId, List<string> images)
        {
            using (var context = new PrimeDatingContext())
            {
                var result = context.GirlsImages
                    .Where(t => t.GirlId == girlId).ToList();

                foreach (var image in images.Where(t => !result.Exists(c => c.Url == t)))
                {
                    context.GirlsImages.AddOrUpdate(new GirlsImages
                    {
                        GirlId = girlId,
                        Url = image
                    });
                }

                context.SaveChanges();
            }
        }

        public void AddOrUpdatePassportScans(int girlId, List<string> passportScans)
        {
            using (var context = new PrimeDatingContext())
            {
                var result = context.GirlsPassportScans
                    .Where(t => t.GirlId == girlId).ToList();

                foreach (var passportScan in passportScans.Where(t => !result.Exists(c => c.Url == t)))
                {
                    context.GirlsPassportScans.AddOrUpdate(new GirlsPassportScans
                    {
                        GirlId = girlId,
                        Url = passportScan
                    });
                }

                context.SaveChanges();
            }
        }

        public void AddOrUpdateGirl(Girls girl)
        {
            using (var context = new PrimeDatingContext())
            {
                context.Girls.AddOrUpdate(girl);

                context.SaveChanges();
            }
        }

        public void AddOrUpdateManagersGirlsReference(List<ManagersGirls> managersGirlsReference)
        {
            using (var context = new PrimeDatingContext())
            {
                foreach (var managerGirlReference in managersGirlsReference)
                {
                    var reference =
                        context.ManagersGirls.Find(managerGirlReference.GirlId, managerGirlReference.ManagerId);

                    if (reference == null)
                    {
                        context.ManagersGirls.Add(new ManagersGirls
                        {
                            Creation = DateTime.Now,
                            GirlId = managerGirlReference.GirlId,
                            ManagerId = managerGirlReference.ManagerId
                        });
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
