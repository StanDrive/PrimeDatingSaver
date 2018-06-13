using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    [Table("Managers_Girls")]
    public class ManagersGirls : Entity
    {
        [Key]
        [Column(Order = 1)]
        public int GirlId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ManagerId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }
    }
}
