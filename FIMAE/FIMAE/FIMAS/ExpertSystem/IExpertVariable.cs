using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.ExpertSystem
{
    [Serializable]
    public abstract class IExpertVariable
    {
        public abstract string Name { get; set; }
        public abstract List<string> Values { get; set; }
    }
}
