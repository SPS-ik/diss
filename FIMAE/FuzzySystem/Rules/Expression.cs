using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySystem.Rules
{
    public class Expression
    {
        public FuzzyVariable Variable;
        public Term Term;

        public Expression(FuzzyVariable v, Term t)
        {
            Variable = v;
            Term = t;
        }
    }
}
