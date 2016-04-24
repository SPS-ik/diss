using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.KnowledgeBase
{
    public class KnowledgeBaseTerm
    {
        public string Name;
        public double A;
        public double B;
        public double C;
        public double D;

        public KnowledgeBaseTerm(string name, double a, double b, double c, double d)
        {
            Name = name;
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }
}
