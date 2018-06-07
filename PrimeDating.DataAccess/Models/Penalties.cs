using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    internal class Penalties
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdminAreaId { get; set; }

        [Required]
        public int GirlId { get; set; }

        [Required]
        public int ManagerId { get; set; }

        [Required]
        public decimal PenaltyAmount { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [ForeignKey("AdminAreaId")]
        public AdminAreas AdminArea { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }
    }
}
