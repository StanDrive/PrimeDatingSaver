using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    internal class GiftOrders
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GiftId { get; set; }

        [Required]
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
