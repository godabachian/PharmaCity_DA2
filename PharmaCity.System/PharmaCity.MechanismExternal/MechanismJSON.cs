using PharmaCity.IMechanism;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PharmaCity.MechanismExternal
{
    public class MechanismJSON : IConcreteMechanism
    {
        public void Export(IEnumerable<Object> medicines)
        {
            string serializedMedicines = JsonSerializer.Serialize(medicines);
            File.WriteAllText("MedicineExported.json", serializedMedicines);
        }
    }
}
