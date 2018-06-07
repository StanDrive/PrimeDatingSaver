using System.ComponentModel.DataAnnotations;

namespace PrimeDating.DataAccess.Models
{
    internal class ContactsRequestStatuses
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
