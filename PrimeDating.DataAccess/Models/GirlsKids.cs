using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    [Table("Girls_Kids")]
    internal class GirlsKids
    {
        [Key]
        [Column(Order = 1)]
        public int GirlId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int KidId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("KidId")]
        public Kids Kid { get; set; }
    }
}
