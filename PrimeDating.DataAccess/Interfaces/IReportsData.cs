using System;
using System.Collections.Generic;
using PrimeDating.Models.Database;
using Payments = PrimeDating.Models.Reports.Payments;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IReportsData
    {
        List<Payments> GetPaymentsWithinRange(DateTime startDate, DateTime endDate);

        List<PaymentTypes> GetPaymentTypes();
    }
}
