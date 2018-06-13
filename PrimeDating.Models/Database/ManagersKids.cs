using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    [Table("Managers_Kids")]
    public class ManagersKids : Entity
    {
        [Key]
        [Column(Order = 1)]
        public int ManagerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int KidId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }

        [ForeignKey("KidId")]
        public Kids Kid { get; set; }
    }
}
