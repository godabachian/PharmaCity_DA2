using System;
using System.Collections.Generic;

namespace PharmaCity.IMechanism
{
    public interface IConcreteMechanism
    {
        void Export(IEnumerable<Object> medicines);
    }
}
