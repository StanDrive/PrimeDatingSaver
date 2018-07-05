using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;
using PrimeDating.Models.Reports;
using Payments = PrimeDating.Models.Reports.Payments;

namespace PrimeDating.DataAccess
{
    public class ReportsData : IReportsData
    {
        public List<Payments> GetPaymentsWithinRange(DateTime startDate, DateTime endDate)
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Payments
                    .Include(t => t.Girl)
                    .Include(t => t.Manager)
                    .Include(t => t.AdminArea)
                    .Where(t => t.Date >= startDate && t.Date <= endDate)
                    .Select(t => new Payments
                    {
                        ManagerId = t.ManagerId.ToString(),
                        ManagerName = t.Manager.Email,
                        AdminAreaName = t.AdminArea.Name,
                        PaymentType = t.PaymentType.Name,
                        GirlId = t.GirlId.ToString(),
                        GirlName = t.Girl.FirstName,
                        Date = t.Date,
                        Amount = t.Amount
                    }).ToList();
            }
        }

        public List<PaymentTypes> GetPaymentTypes()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.PaymentTypes.ToList();
            }
        }

        public List<Logging> GetLogsByPeriod(DateTime startDate, DateTime endDate)
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Logging.Where(t => t.DateTime >= startDate && t.DateTime <= endDate).ToList();
            }
        }

        public List<Manager> GetAllManagers()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Managers
                    .Include(t => t.AdminArea)
                    .Select(t => new Manager
                    {
                        ManagerId = t.Id.ToString(),
                        ManagerName = t.Email,
                        AdminArea = t.AdminArea.Name
                    }).ToList();
            }
        }

        public List<Girl> GetAllGirls()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Girls
                    .Include(t => t.AdminArea)
                    .Select(t => new Girl
                    {
                        GirlId = t.Id.ToString(),
                        GirlName = t.FirstName,
                        AdminArea = t.AdminArea.Name
                    }).ToList();
            }
        }
    }
}
