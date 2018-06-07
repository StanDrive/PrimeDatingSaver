using System.ComponentModel.DataAnnotations;

namespace PrimeDating.DataAccess.Models
{
    internal class Orders
    {
        [Key]
        [Range(1, 9999, ErrorMessage = "Value must be between 1 to 9999")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(2000)]
        public string Picture { get; set; }
    }
}
