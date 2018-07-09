using System;

namespace PrimeDating.Models.Reports
{
    public class Gift
    {
        public DateTime GiftDate { get; set; }

        public string ManId { get; set; }

        public string GirlId { get; set; }

        public string ManagerId { get; set; }

        public string GiftName { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }
    }
}
