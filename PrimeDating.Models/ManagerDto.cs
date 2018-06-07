using System.Collections.Generic;

namespace PrimeDating.Models
{
    public class ManagerDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Users { get; set; }
    }
}
