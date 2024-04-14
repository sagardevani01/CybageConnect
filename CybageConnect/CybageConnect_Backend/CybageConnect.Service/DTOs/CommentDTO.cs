using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreationDate { get; set; }
        public string? UserName { get; set; }
    }
}
