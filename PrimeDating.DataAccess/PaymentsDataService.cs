using System.Collections.Generic;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.DataAccess.Models;

namespace PrimeDating.DataAccess
{
    internal class PaymentsDataService : IPaymentsDataService
    {
        public void AddOrUpdatePayments(List<Payments> payments)
        {
            using (var context = new PrimeDatingContext())
            {
                foreach (var payment in payments)
                {
                    if (!context.Payments.Any(t =>
                        t.AdminAreaId == payment.AdminAreaId && t.Date == payment.Date && t.GirlId == payment.GirlId &&
                        t.ManagerId == payment.ManagerId && t.PaymentTypeId == payment.PaymentTypeId))
                    {
                        context.Payments.Add(payment);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
