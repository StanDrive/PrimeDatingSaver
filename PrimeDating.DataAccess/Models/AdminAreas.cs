using System.ComponentModel.DataAnnotations;

namespace PrimeDating.DataAccess.Models
{
    internal class AdminAreas
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }
    }
}
