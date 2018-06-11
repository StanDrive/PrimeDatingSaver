using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    public class MeetingRequests
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdminAreaId { get; set; }

        [Required]
        public int ManId { get; set; }

        [Required]
        public int GirlId { get; set; }

        [Required]
        public int ManagerId { get; set; }

        public DateTime? MeetingApprovalDate { get; set; }

        [Required]
        public int MeetingRequestStatusId { get; set; }

        [ForeignKey("AdminAreaId")]
        public AdminAreas AdminArea { get; set; }

        [ForeignKey("ManId")]
        public Men Man { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }

        [ForeignKey("ManagerId")]
        public Managers Manager { get; set; }

        [ForeignKey("MeetingRequestStatusId")]
        public MeetingRequestStatuses MeetingRequestStatus { get; set; }
    }
}
