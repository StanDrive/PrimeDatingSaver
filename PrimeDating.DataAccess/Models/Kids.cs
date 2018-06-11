using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeDating.DataAccess.Models
{
    public class Kids
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        public DateTime? BirthDay { get; set; }
    }
}
