using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.DTOs
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int PostId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
