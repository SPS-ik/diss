using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.DefiningFeatures
{
    public class DefiningFeature
    {
        public string Name;
        public List<string> Values;

        public DefiningFeature()
        {
        }

        public DefiningFeature(string name, List<string> values)
        {
            Name = name;
            Values = values;
        }
    }
}
