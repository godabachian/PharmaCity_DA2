using System.Collections.Generic;

namespace PharmaCity.Domain.DTO
{
    public class StockRequestDTO
    {
        public int Id { get; set; }
        public string EmployeeUserName { get; set; }
        public IEnumerable<Petition> Petitions { get; set; }
    }
}