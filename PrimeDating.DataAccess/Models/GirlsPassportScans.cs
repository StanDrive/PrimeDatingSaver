using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    internal class GirlsPassportScans
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GirlId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Url { get; set; }

        [ForeignKey("GirlId")]
        public Girls Girl { get; set; }
    }
}
