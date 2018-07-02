using System;

namespace PrimeDating.Models.Reports
{
    public class Payments
    {
        public string ManagerName { get; set; }

        public string ManagerId { get; set; }

        public string GirlId { get; set; }

        public string GirlName { get; set; }

        public string AdminAreaName { get; set; }

        public string PaymentType { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }
    }
}
