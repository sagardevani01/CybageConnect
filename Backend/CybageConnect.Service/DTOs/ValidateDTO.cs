using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybageConnect.Service.DTOs
{
    public class ValidateDTO
    {
        public string EmailError {  get; set; }
        public string UsernameError { get; set; }
        public string PhoneError { get; set; }
       
    }
}
