using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    public class Gifts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string GiftLink { get; set; }

        [Required]
        public DateTime GiftStatusUpdateDate { get; set; }

        [Required]
        public int GiftStatusId { get; set; }

        [Required]
        public int AdminAreaId { get; set; }

        [Required]
        public int ManId { get; set; }

        [Required]
        public int GirlId { get; set; }

        [Required]
        public int ManagerId { get; set; }

        [ForeignKey("ManId")]
        public Men Man { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }

        [ForeignKey("GiftStatusId")]
        public GiftStatus Status { get; set; }

        [ForeignKey("AdminAreaId")]
        public AdminAreas AdminArea { get; set; }
    }
}
