﻿using System;
using System.Collections.Generic;
using PrimeDating.Models.Database;
using PrimeDating.Models.Reports;
using Payments = PrimeDating.Models.Reports.Payments;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IReportsData
    {
        List<Payments> GetPaymentsWithinRange(DateTime startDate, DateTime endDate);

        List<PaymentTypes> GetPaymentTypes();

        List<Logging> GetLogsByPeriod(DateTime startDate, DateTime endDate);

        List<Manager> GetAllManagers();

        List<Girl> GetAllGirls();

        List<Gift> GetGiftsByPeriod(DateTime startDate, DateTime endDate);
    }
}
