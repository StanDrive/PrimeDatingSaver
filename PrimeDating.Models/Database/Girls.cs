using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    public class Girls : Entity
    {
        [Key]
        [Range(1, 999999999, ErrorMessage = "Value must be between 1 to 999999999")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public int AssignedManagerId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Passport { get; set; }

        [MaxLength(100), MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100), MinLength(3)]
        public string FirstName { get; set; }

        [MaxLength(100), MinLength(3)]
        public string Patronymic { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public int ChildrenCount { get; set; }

        [MaxLength(250)]
        public string InstagramLink { get; set; }

        [MaxLength(250)]
        public string FacebookLink { get; set; }

        [MaxLength(250)]
        public string VkLink { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        [MaxLength(25)]
        public string BodyType { get; set; }

        [Required]
        [MaxLength(25)]
        public string MartialStatus { get; set; }

        [Required]
        [MaxLength(50)]
        public string Education { get; set; }

        [Required]
        [MaxLength(30)]
        public string Religion { get; set; }

        [Required]
        [MaxLength(30)]
        public string Smoking { get; set; }

        [Required]
        [MaxLength(30)]
        public string Drinking { get; set; }

        [Required]
        [MaxLength(30)]
        public string ColorEye { get; set; }

        [Required]
        [MaxLength(30)]
        public string ColorHair { get; set; }

        [Required]
        [MaxLength(2000)]
        public string LookingFor { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(30)]
        public string EngLevel { get; set; }

        [MaxLength(50)]
        public string OtherLangs { get; set; }

        [Required]
        [MaxLength(200)]
        public string WorkPlace { get; set; }

        [MaxLength(500)]
        public string Hobby { get; set; }

        [Required]
        public int AdminAreaId { get; set; }

        [MaxLength(2000)]
        public string Avatar { get; set; }

        [Required]
        public bool CanReceiveGifts { get; set; }

        [ForeignKey("AdminAreaId")]
        public AdminAreas AdminArea { get; set; }

        [ForeignKey("AssignedManagerId")]
        public Managers AssignedManager { get; set; }
    }
}
