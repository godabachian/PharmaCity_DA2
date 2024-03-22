using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.DTO
{
    public class InvitationDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Code { get; set; }
        public string PharmacyName { get; set; }
        public string State { get; set; }

    }
}
