using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.DTOs
{
    public class UserDTO
    {
       public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string Username { get; set; } = null!;

        public string? Password { get; set; } 

        public string Email { get; set; } = null!;

        public string? Designation { get; set; }

        public string? Departament { get; set; }

        public string? ProfilePicture { get; set; }

        public bool IsActive { get; set; }

        public string? Location { get; set; }
        public string Phone {  get; set; }
    }
}
