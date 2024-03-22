using PharmaCity.Domain;
using PharmaCity.Domain.DTO;
using PharmaCity.IBusinessLogic;
using PharmaCity.IDataAccess;
using PharmaCity.IMechanism;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCity.BusinessLogic
{
    public class ExportService : IExportService
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly string _exportersPath;

        public ExportService(IMedicineRepository medicineRepository)
        {
            this._medicineRepository = medicineRepository;
            this._exportersPath = Directory.GetCurrentDirectory() + @"\bin\Debug\net5.0\PharmaCity.MechanismExternal.dll";
        }

        public IEnumerable<string> GetExporters()
        {
            Assembly assembly = Assembly.LoadFile(_exportersPath);
            IEnumerable<Type> mechanisms = GetMechanismsAssembly(assembly);

            return GetNamesOfMechanisms(mechanisms);
        }

        private IEnumerable<string> GetNamesOfMechanisms(IEnumerable<Type> mechanisms)
        {
            List<string> mechanismsNames = new List<string>();

            foreach (var mechanism in mechanisms)
            {
                mechanismsNames.Add(mechanism.Name);
            }
            return mechanismsNames;
        }

        public void Export(string mechanismName)
        {
            Assembly assembly = Assembly.LoadFile(_exportersPath);
            IEnumerable<Type> mechanisms = GetMechanismsAssembly(assembly);
            Type useMechanism = mechanisms.FirstOrDefault(mechanism => mechanism.Name == mechanismName);
            IConcreteMechanism exporterMechanism = (IConcreteMechanism)Activator.CreateInstance(useMechanism);
            IEnumerable<Medicine> medicines = _medicineRepository.GetMedicines();
            exporterMechanism.Export(medicines);
        }

        private IEnumerable<Type> GetMechanismsAssembly(Assembly assembly)
        {
            List<Type> types = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IConcreteMechanism).IsAssignableFrom(type))
                {
                    types.Add(type);
                }
            }
            return types;
        }
    }
}
