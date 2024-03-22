using PharmaCity.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.BusinessLogic.Tools
{
    public class GuidService : IGuidService
    {
        private Random _random;
        private string _characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public GuidService()
        {
            _random = new Random();
        }

        public string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public string RandomCode()
        {
            return _random.Next(100000, 999999).ToString();
        }

        public string RandomCodeMedicine()
        {
            var Charsarr = new char[5];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = _characters[random.Next(_characters.Length)];
            }

            var resultString = new String(Charsarr);
            return resultString;
        }

    }
}
