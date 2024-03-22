using System;

namespace PharmaCity.Domain
{
    public class Invitation
    {
        private string _userName;
        private int? _minimumLength = 1;
        private int? _maximumLength = 20;

        public int Id { get; set; }

        public string UserName
        {
            get => _userName;

            set
            {
                if (value == null) throw new ArgumentNullException("El nombre de usuario no debe ser nulo");
                    
                if (value.Contains(" ")) throw new InvalidOperationException("El nombre de usuario no debe contener espacios");

                if (value?.Length < _minimumLength || value?.Length > _maximumLength) throw new InvalidOperationException($"El nombre de usuario debe contener como mínimo {_minimumLength} caracteres y máximo de {_maximumLength} caracteres");
                    
                _userName = value;
            }
        }

        public RoleType Role { get; set; }

        public string Code { get; set; }

        public Pharmacy Pharmacy { get; set; }

        public State State { get; set; }
    }
}