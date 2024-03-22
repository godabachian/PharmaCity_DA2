using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.DTO.IN
{
    public class InvitationIN
    {
        public string UserName { get; set; }
        public RoleType Role { get; set; }
        public string PharmacyName { get; set; }
    }
}
