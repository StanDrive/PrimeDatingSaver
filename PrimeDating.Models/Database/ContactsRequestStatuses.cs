using System.ComponentModel.DataAnnotations;

namespace PrimeDating.Models.Database
{
    public class ContactsRequestStatuses : Entity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
