using System;
using System.Collections.Generic;

namespace PrimeDating.Models
{
    public class GiftDto
    {
        public string GiftLink { get; set; }

        public string GiftId { get; set; }

        public string OperatorId { get; set; }

        public string MaleId { get; set; }

        public string FemaleId { get; set; }

        public DateTime GiftStatusUpdateDate { get; set; }

        public string GiftStatus { get; set; }

        public List<OrderDto> Orders { get; set; }
    }
}
