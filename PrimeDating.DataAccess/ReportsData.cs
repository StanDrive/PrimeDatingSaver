using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;
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
    }
}
