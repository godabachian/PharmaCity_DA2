using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.IBusinessLogic
{
    public interface IExportService
    {
        void Export(string mechanismName);
        IEnumerable<string> GetExporters();
    }
}
