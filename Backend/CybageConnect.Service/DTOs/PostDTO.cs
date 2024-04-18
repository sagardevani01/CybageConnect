using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public DateTime CreationDate { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? UserName { get; set; }

        public string? UserProfile {  get; set; }
    }
}
