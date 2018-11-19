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
                        ManagerId = t.ManagerId,
                        ManagerEmail = t.Manager.Email,
                        ManagerFullName = t.Manager.FirstName + " " + t.Manager.LastName,
                        AdminAreaName = t.AdminArea.Name,
                        PaymentType = t.PaymentTypeId,
                        GirlId = t.GirlId,
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
                        ManagerId = t.Id,
                        ManagerName = t.Email,
                        CreationDate = t.Creation,
                        AdminArea = t.AdminArea.Name
                    })
                    .OrderBy(t => t.AdminArea)
                    .ToList();
            }
        }

        public List<Girl> GetAllGirls()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Girls
                    .Include(t => t.AdminArea)
                    .Include(t => t.AssignedManager)
                    .Select(t => new Girl
                    {
                        GirlId = t.Id,
                        GirlName = t.FirstName,
                        AdminArea = t.AdminArea.Name,
                        AssignedManager = t.AssignedManager.Email,
                        AssignedManagerId = t.AssignedManagerId,
                        FullName = t.FirstName + " " + t.LastName
                    })
                    .OrderBy(t => t.AdminArea)
                    .ToList();
            }
        }

        public List<Gift> GetGiftsByPeriod(DateTime startDate, DateTime endDate)
        {
            using (var context= new PrimeDatingContext())
            {
                return context.GiftOrders
                    .Include(t => t.Gift)
                    .Include(t => t.Gift.AdminArea)
                    .Include(t => t.Order)
                    .Where(t => t.Gift.GiftStatusUpdateDate >= startDate && t.Gift.GiftStatusUpdateDate <= endDate)
                    .Select(t => new Gift
                    {
                        GiftName = t.Order.Name,
                        GirlId = t.Gift.GirlId,
                        ManagerId = t.Gift.ManagerId,
                        Amount = t.Amount,
                        GiftDate = t.Gift.GiftStatusUpdateDate,
                        ManId = t.Gift.ManId,
                        Price = t.Price,
                        AdminArea = t.Gift.AdminArea.Name
                    }).ToList();
            }
        }

        public List<AdminAreaStatistic> GetAdminAreasStatistics()
        {
            using (var context = new PrimeDatingContext())
            {
                var totalPayments = context.Payments.Sum(t => t.Amount);

                var result = context.Payments.Include(t => t.AdminArea)
                    .GroupBy(t => t.AdminArea.Name)
                    .Select(t => new AdminAreaStatistic
                    {
                        AdminAreaName = t.Key,
                        PaymentsSum = t.Sum(s => s.Amount)
                    }).ToList();

                foreach (var adminAreaStatistic in result)
                {
                    adminAreaStatistic.Percentage =
                        totalPayments == 0 ? 0 : adminAreaStatistic.PaymentsSum / totalPayments * 100;
                }

                return result;
            }
        }
    }
}
