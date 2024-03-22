using PharmaCity.BusinessLogic.Tools;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;

namespace PharmaCity.BusinessLogic
{
    public class SessionService : ISessionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGuidService _guidService;
        private readonly HashService _hash;

        public SessionService(IUserRepository userRepository, IGuidService guidService)
        {
            _userRepository = userRepository;
            _guidService = guidService;
            _hash = new HashService();
        }

        public bool IsAllowed(RoleType role, string token)
        {
            return _userRepository.IsAllowed(role, token);
        }

        public LoginDTO Login(Login login)
        {
            login.Password = _hash.GetHash(login.Password);

            if (_userRepository.ValidLogin(login.Email, login.Password))
            {
                User userLoged = _userRepository.GetUserByEmail(login.Email);
                userLoged.Token = _guidService.NewGuid();
                _userRepository.Update(userLoged);

                LoginDTO loginDTO = new LoginDTO
                {
                    Token = userLoged.Token,
                    User = userLoged.UserName,
                    Role = userLoged.Role.ToString()
                };

                return loginDTO;
            }
            else
            {
                throw new InvalidOperationException("El correo o la contraseña son incorrectos");
            }
        }
    }
}