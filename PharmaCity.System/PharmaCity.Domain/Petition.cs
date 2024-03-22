using System;

namespace PharmaCity.Domain
{
    public class Petition
    {
        private int _quantity;

        public int Id { get; set; }
        public string MedicineCode { get; set; }
        public int Quantity 
        { 
            get => _quantity;

            set
            {
                if (value > 0)
                {
                    _quantity = value;
                }
                else
                {
                    throw new InvalidOperationException("La cantidad de la peticion debe ser mayor a 0");
                }
                
            }
        }
        public Pharmacy Pharmacy { get; set; }
        public State State { get; set; }
    }
}