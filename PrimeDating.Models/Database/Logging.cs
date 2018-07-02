using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models.Database
{
    [Table("Logging")]
    public class Logging : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        [MaxLength(10)]
        public string Level { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }

        [MaxLength(1000)]
        public string Exception { get; set; }
    }
}
