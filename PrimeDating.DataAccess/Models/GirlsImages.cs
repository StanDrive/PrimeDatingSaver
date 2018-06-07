using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    internal class GirlsImages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Url { get; set; }

        [Required]
        public int GirlId { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }
    }
}
