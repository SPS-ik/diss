using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FIMAS.ExpertSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.Limits
{

    [Serializable]
    public class Limit : IExpertVariable
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
        public DefiningFeature DependedFeature { get; set; }

        public Limit()
        {
            Values = new List<string>();
        }

        public Limit(string name, List<string> values, DefiningFeature dependedFeature)
        {
            Name = name;
            Values = values;
            DependedFeature = dependedFeature;
        }
    }
}
