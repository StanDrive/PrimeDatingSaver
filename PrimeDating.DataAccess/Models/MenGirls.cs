using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    [Table("Men_Girls")]
    internal class MenGirls
    {
        [Key]
        [Column(Order = 1)]
        public int ManId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int GirlId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [ForeignKey("ManId")]
        public Men Man { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }
    }
}
