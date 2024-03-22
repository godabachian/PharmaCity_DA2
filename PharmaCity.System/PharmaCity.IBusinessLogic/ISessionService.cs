using PharmaCity.Domain;
using PharmaCity.Domain.DTO;

namespace PharmaCity.IBusinessLogic
{
    public interface ISessionService
    {
        bool IsAllowed(RoleType role, string token);
        LoginDTO Login(Login login);
    }
}