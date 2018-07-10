using System;

namespace PrimeDating.Models.Reports
{
    public class Gift
    {
        public DateTime GiftDate { get; set; }

        public int ManId { get; set; }

        public int GirlId { get; set; }

        public int ManagerId { get; set; }

        public string AdminArea { get; set; }

        public string GiftName { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }
    }
}
