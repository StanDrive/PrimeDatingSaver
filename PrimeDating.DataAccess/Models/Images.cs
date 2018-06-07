using System.ComponentModel.DataAnnotations;

namespace PrimeDating.DataAccess.Models
{
    internal class Images
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Url { get; set; }
    }
}
