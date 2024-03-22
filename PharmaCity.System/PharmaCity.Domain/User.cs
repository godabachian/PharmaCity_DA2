using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PharmaCity.Domain
{
    public class User
    {
        private string _email;
        private string _password;
        private string _userName;

        private int _minimumLengthEmail = 8;
        private int _minimumLengthPassword = 8;
        private int _minimumLength = 1;
        private int _maximumLength = 20;

        Regex specialCharacters = new Regex("[!\"#\\$%&'()*+,-./:;=?@\\[\\]^_`{|}~]");

        public int Id { get; set; }

        public string Email 
        { 
            get => _email;

            set
            {
                if (value?.Length < _minimumLengthEmail) 
                    throw new InvalidOperationException($"El email debe tener como mínimo {_minimumLengthEmail} caracteres");
                if (!new EmailAddressAttribute().IsValid(value))
                    throw new InvalidOperationException("El email no es correcto");

                _email = value?? throw new ArgumentNullException("El email no debe ser nulo");
            } 
        }

        public string UserName 
        { 
            get => _userName;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("El nombre de usuario no debe ser nulo");
                }

                if (value.Contains(" "))
                {
                    throw new InvalidOperationException("El nombre de usuario no debe contener espacios");
                }

                if (value?.Length < _minimumLength || value?.Length > _maximumLength)
                {
                    throw new InvalidOperationException($"El nombre de usuario debe contener como mínimo {_minimumLength} caracteres y máximo de {_maximumLength} caracteres");
                }

                _userName = value;
            }
        }

        public string Direction { get; set; }

        public string Password 
        { 
            get => _password;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("La contraseña no debe ser nula");
                }
                if (!specialCharacters.IsMatch(value))
                {
                    throw new InvalidOperationException("La contraseña debe contener al menos un caracter especial");
                }
                if (value?.Length < _minimumLengthPassword)
                {
                    throw new InvalidOperationException("La contraseña debe tener como minimo 8 caracteres");
                }
                else
                {
                    _password = value;
                }
                
            }
        }

        public string Token { get; set; }

        public DateTime RegisterDate { get; set; }
        public RoleType Role { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}