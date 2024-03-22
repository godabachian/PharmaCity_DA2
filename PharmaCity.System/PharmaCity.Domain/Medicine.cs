using System;

namespace PharmaCity.Domain
{
    public class Medicine
    {
        private string _code;
        private string _name;
        private string _symptoms;
        private string _presentation;
        private string _unit;
        private string _receipt;
        private int _maximumLength = 30;
        private int _maximumUnitLength = 5;
        private int _maximumReceiptLength = 4;

        public int Id { get; set; }

        public string Code 
        { 
            get => _code;

            set
            {
                _code = value ?? throw new ArgumentNullException("El codigo no puede ser nulo");
            }
        }
        public string Name
        {
            get => _name;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("El nombre de medicamento no puede ser nulo");
                }
                else
                {
                    if (value?.Length > _maximumLength || value?.Length == 0)
                    {
                        throw new InvalidOperationException("El nombre del medicamento debe tener un largo de entre 1 y 30 caracteres");
                    }

                    _name = value;
                }

                
            }
        }

        public string Symptoms
        {
            get => _symptoms;

            set
            {
                _symptoms = value ?? throw new ArgumentNullException("Los sintomas no puede ser nulo");
            }
        }

        public string Presentation
        {
            get => _presentation;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("La presentacion no puede ser nula");
                }
                else
                {
                    if (value?.Length > _maximumLength || value?.Length == 0)
                    {
                        throw new InvalidOperationException("La presentacion debe tener entre 1 y 30 caracteres");
                    }

                    _presentation = value;
                }

            }
        }

        public int Quantity { get; set; }

        public string Unit
        {
            get => _unit;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("La unidad no puede ser nula");
                }
                else
                {
                    if (value?.Length > _maximumUnitLength || value?.Length == 0)
                    {
                        throw new InvalidOperationException("La unidad debe tener entre 1 y 5 caracteres");
                    }

                    _unit = value;
                }

            }
        }

        public int Price { get; set; }

        public int Stock { get; set; }

        public string Receipt
        {
            get => _receipt;

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("La receta no puede ser nula");
                }
                else
                {
                    if (value?.Length > _maximumReceiptLength || value?.Length == 0)
                    {
                        throw new InvalidOperationException("La receta es Si o No");
                    }

                    _receipt = value;
                }
            }
        }

        public string PharmacyName { get; set; }
    }
}