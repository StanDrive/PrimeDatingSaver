using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    public class GiftOrders : Entity
    {
        [Key]
        [Column(Order = 1)]
        public int GiftId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int OrderId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("GiftId")]
        public Gifts Gift { get; set; }

        [ForeignKey("OrderId")]
        public Orders Order { get; set; }
    }
}
