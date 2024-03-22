using PharmaCity.BusinessLogic.Tools;
using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace PharmaCity.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly HashService _hash;

        public UserService(IUserRepository userRepository, IInvitationRepository invitationRepository)
        {
            _userRepository = userRepository;
            _invitationRepository = invitationRepository;
            _hash = new HashService();
        }

        public UserDTO InsertUser(User user, string code)
        {
            CheckExistsUser(user.Email);
            CheckExistsByCodeAndUserName(code, user.UserName);

            Invitation invitationInDataBase = _invitationRepository.GetInvitationByCode(code);

            user.Role = invitationInDataBase.Role;
            user.Pharmacy = invitationInDataBase.Pharmacy;
            user.RegisterDate = DateTime.Now;
            user.Password = _hash.GetHash(user.Password);
            _userRepository.InsertUser(user);

            invitationInDataBase.State = State.Inactive;
            _invitationRepository.Update(invitationInDataBase);

            return GetUserDTO(user);

        }

        private void CheckExistsByCodeAndUserName(string code, string userName)
        {
            if (!(_invitationRepository.ExistsByCodeAndUserName(code, userName)))
            {
                throw new NullReferenceException("La invitacion con dicho código/usuario no existe");
            }
        }

        private void CheckExistsUser(string email)
        {
            if (_userRepository.Exists(email))
            {
                throw new InvalidOperationException("El correo electrónico ya existe");
            }
        }

        private UserDTO GetUserDTO(User user)
        {
            UserDTO userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Direction = user.Direction,
                Role = user.Role
            };

            return userDTO;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();

            foreach (var user in _userRepository.GetUsers())
            {
                userDTOs.Add(GetUserDTO(user));
            }

            return userDTOs;
        }

        public void DeleteUser(string email)
        {
            CheckNonExistsUser(email);
            User user = _userRepository.GetUserByEmail(email);
            _userRepository.DeleteUser(user);
        }

        private void CheckNonExistsUser(string email)
        {
            if (!(_userRepository.Exists(email)))
            {
                throw new InvalidOperationException("No existe un usuario con ese email");
            }
        }

        public User GetUserByEmail(string email)
        {
            CheckNonExistsUser(email);
            User user = _userRepository.GetUserByEmail(email);
            return user;
        }
    }
}
