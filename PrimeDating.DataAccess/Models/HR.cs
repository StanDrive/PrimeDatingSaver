using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    [Table("HR")]
    public class HR
    {
        [Key]
        [Range(10000, 99999, ErrorMessage = "Value must be between 10000 to 99999")]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Skype { get; set; }

        [Required]
        [MaxLength(200)]
        public string InfoSource { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        [MaxLength(300)]
        public string LivingArea { get; set; }
    }
}
