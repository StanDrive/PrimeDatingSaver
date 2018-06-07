using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    internal class CorrespondenceDailyBalance
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
        public DateTime BalanceDate { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [ForeignKey("AdminAreaId")]
        public AdminAreas AdminArea { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }
    }
}
