using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.ExpertSystem
{
    public interface IExpertVariable
    {
        string Name {get; set;}
        List<string> Values { get; set; }
    }
}
