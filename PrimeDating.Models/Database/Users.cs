using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    public class Users : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public bool IsBlock { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
