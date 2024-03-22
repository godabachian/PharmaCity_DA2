using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.DTO
{
    public class PetitionDTO
    {
        public int Id { get; set; }
        public string MedicineCode { get; set; }
        public int Quantity { get; set; }
        public string PharmacyName { get; set; }
        public string State { get; set; }
    }
}
