using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    public class Payments : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AdminAreaId { get; set; }

        [Required]
        public int GirlId { get; set; }

        [Required]
        public int ManagerId { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }

        [ForeignKey("PaymentTypeId")]
        public PaymentTypes PaymentType { get; set; }

        [ForeignKey("AdminAreaId")]
        public AdminAreas AdminArea { get; set; }
    }
}
