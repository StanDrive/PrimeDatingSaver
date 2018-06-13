using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeDating.Models.Database
{
    public class Kids : Entity
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
