using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.DTO
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public string PurchaseCode { get; set; }
        public string PharmacyName { get; set; }
        public IEnumerable<PetitionBuy> Shopping { get; set; }
        public string State { get; set; }
    }
}
