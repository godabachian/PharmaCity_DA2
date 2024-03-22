using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain.DTO.IN
{
    public class MedicineIN
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string Symptoms { get; set; }
        public string Unit { get; set; }
        public string Receipt { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
