using PharmaCity.DataAccess.Context;
using PharmaCity.Domain;
using PharmaCity.IDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace PharmaCity.DataAccess
{
    public class PetitionRepository : IPetitionRepository
    {
        private PharmaCityDbContext _dbContext;

        public PetitionRepository(PharmaCityDbContext context)
        {
            this._dbContext = context;
        }

        public IEnumerable<Petition> GetPetitions()
        {
            return _dbContext.Petitions.ToList();
        }

        public void InsertPetition(Petition petition)
        {
            _dbContext.Petitions.Add(petition);
            _dbContext.SaveChanges();
        }

        public void DeletePetition(Petition petition)
        {
            _dbContext.Petitions.Remove(petition);
            _dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _dbContext.Petitions.Any(petition => petition.Id == id);
        }

        public Petition GetPetitionById(int id)
        {
            return _dbContext.Petitions.FirstOrDefault(petition => petition.Id == id);
        }

        public IEnumerable<Petition> GetPetitionsBuy(string pharmacyName)
        {
            return _dbContext.Petitions.Where(petition => petition.Pharmacy.Name == pharmacyName && petition is PetitionBuy).ToList();
        }

        public void Update(Petition petition)
        {
            _dbContext.Petitions.Update(petition);
            _dbContext.SaveChanges();
        }
    }
}