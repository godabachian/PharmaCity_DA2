using System;
using System.Collections.Generic;

namespace PharmaCity.Domain
{
    public class Pharmacy
    {
        private string _name;

        private int _minimumLength = 1;
        private int _maximumLength = 50;
        
        public int Id { get; set; }

        public string Name { 

            get => _name;

            set
            {
                if (value?.Length < _minimumLength || value?.Length > _maximumLength)
                {
                    throw new InvalidOperationException($"El nombre de la farmacia debe contener mínimo {_minimumLength} caracteres y máximo de {_maximumLength} caracteres");
                }
                _name = value?? throw new ArgumentNullException("El nombre de la farmacia no puede ser nulo");
            }
        }
        public string Direction { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
    }
}