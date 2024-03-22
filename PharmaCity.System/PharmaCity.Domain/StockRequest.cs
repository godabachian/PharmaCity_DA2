using System.Collections.Generic;

namespace PharmaCity.Domain
{
    public class StockRequest
    {
        public int Id { get; set; }
        public User Employee { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public IEnumerable<PetitionStock> Petitions { get; set; }
        public State State { get; set; }
    }
}