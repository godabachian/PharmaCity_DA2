using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.Domain
{
    public class Purchase
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public IEnumerable<PetitionBuy> Shopping { get; set; }
        public State State { get; set; }
    }
}
