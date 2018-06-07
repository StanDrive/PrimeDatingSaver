using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    internal class Managers
    {
        [Key]
        [Range(1000, 99999, ErrorMessage = "Value must be between 10000 to 99999")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string Patronymic { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(250)]
        public string InstagramLink { get; set; }

        [MaxLength(250)]
        public string FacebookLink { get; set; }

        [MaxLength(250)]
        public string VkLink { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(25)]
        public string MartialStatus { get; set; }

        [MaxLength(100)]
        public string Skype { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(16)]
        public string BankCard { get; set; }

        [MaxLength(400)]
        public string Bank { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Roles Role { get; set; }
    }
}
