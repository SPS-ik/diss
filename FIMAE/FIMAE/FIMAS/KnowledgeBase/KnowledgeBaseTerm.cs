using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.KnowledgeBase
{
    public class KnowledgeBaseTerm
    {

        public string Name { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }

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
