using System.Collections.Generic;
using PrimeDating.Models.Database;

namespace PrimeDating.BusinessLayer
{
    internal class DailyData
    {
        public int AdminAreaId { get; set; }

        public List<Girls> Girls { get; set; }

        public List<Managers> Managers { get; set; }

        public List<Gifts> Gifts { get; set; }

        public List<GiftOrders> GiftOrders { get; set; }

        public List<Men> Men { get; set; }

        public List<Payments> Payments { get; set; }

        public List<GirlsPassportScans> GirlsPassportScans { get; set; }

        public List<GirlsImages> GirlImages { get; set; }

        public List<ManagersGirls> ManagersGirls { get; set; }

        public List<Orders> Orders { get; set; }
    }
}
