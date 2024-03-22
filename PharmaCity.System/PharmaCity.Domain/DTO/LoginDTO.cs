using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.DTO
{
    public class LoginDTO
    {
        public string User { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
