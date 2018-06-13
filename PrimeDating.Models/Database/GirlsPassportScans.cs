using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    public class GirlsPassportScans : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
