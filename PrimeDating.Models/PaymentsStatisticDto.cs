using System.Collections.Generic;

namespace PrimeDating.Models
{
    public class PaymentsStatisticDto
    {
        public string Id { get; set; }

        public Dictionary<string, List<PaymentItemDto>> Statistic { get; set; }
    }
}
