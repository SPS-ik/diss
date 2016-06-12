using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.ExpertSystem
{
    public class ExpertExpression
    {
        public IExpertVariable Variable { get; set; }
        public string Value { get; set; }

        public ExpertExpression(IExpertVariable var, string val)
        {
            Variable = var;
            Value = val;
        }
    }
}
