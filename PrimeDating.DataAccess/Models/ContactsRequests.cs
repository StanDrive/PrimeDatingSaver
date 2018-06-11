using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    public class ContactsRequests
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
        public int ManId { get; set; }

        [Required]
        public DateTime Creation { get; set; }

        [Required]
        public int ContactsRequestStatusId { get; set; }

        [ForeignKey("AdminAreaId")]
        public AdminAreas AdminArea { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }

        [ForeignKey("ManId")]
        public Men Man { get; set; }

        [ForeignKey("ContactsRequestStatusId")]
        public ContactsRequestStatuses ContactsRequestStatus { get; set; }
    }
}
