using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.DataAccess.Models
{
    public class Men
    {
        [Key]
        [Range(1, 9999999, ErrorMessage = "Value must be between 1 to 9999999")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string Patronymic { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        [MaxLength(150)]
        public string Location { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(25)]
        public string MartialStatus { get; set; }

        [MaxLength(150)]
        public string Children { get; set; }

        [Required]
        [MaxLength(50)]
        public string Religion { get; set; }

        [MaxLength(50)]
        public string Education { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string WorkPlace { get; set; }

        [Required]
        [MaxLength(50)]
        public string Drinking { get; set; }

        [Required]
        [MaxLength(50)]
        public string Smoking { get; set; }

    }
}
