using System.Collections.Generic;

namespace PrimeDating.Models
{
    public class DailyDataDto
    {
        public string Account { get; set; }

        public List<ManagerDto> Managers { get; set; }

        public List<GirlDto> Girls { get; set; }

        public List<GiftDto> Gifts { get; set; }

        public List<MenDto> Men { get; set; }

        public List<PaymentsStatisticDto> PaymentsStatistic { get; set; }
    }
}
