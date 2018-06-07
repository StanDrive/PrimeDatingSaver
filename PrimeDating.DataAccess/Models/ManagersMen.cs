using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    [Table("Managers_Men")]
    internal class ManagersMen
    {
        [Key]
        [Column(Order = 1)]
        public int ManagerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ManId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [ForeignKey("ManId")]
        public Men Man { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }
    }
}
