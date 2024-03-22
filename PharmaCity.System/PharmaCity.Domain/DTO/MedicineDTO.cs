using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.DTO
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Pharmacy { get; set; }
    }
}
