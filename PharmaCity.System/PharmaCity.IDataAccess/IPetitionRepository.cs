using PharmaCity.Domain;
using System.Collections.Generic;

namespace PharmaCity.IDataAccess
{
    public interface IPetitionRepository
    {
        void InsertPetition(Petition petition);
        IEnumerable<Petition> GetPetitions();
        void DeletePetition(Petition petition);
        bool Exists(int id);
        Petition GetPetitionById(int id);
        IEnumerable<Petition> GetPetitionsBuy(string pharmacyName);
        void Update(Petition petition);
    }
}