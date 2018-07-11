using System;

namespace PrimeDating.Models.Reports
{
    public class Payments
    {
        public string ManagerEmail { get; set; }

        public string ManagerFullName { get; set; }

        public int ManagerId { get; set; }

        public int GirlId { get; set; }

        public string GirlName { get; set; }

        public string AdminAreaName { get; set; }

        public int PaymentType { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }
    }
}
