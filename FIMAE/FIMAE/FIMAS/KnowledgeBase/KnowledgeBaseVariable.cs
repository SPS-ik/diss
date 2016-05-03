using FuzzySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.KnowledgeBase
{
    public class KnowledgeBaseVariable
    {
        public string Name { get; set; }
        public List<KnowledgeBaseTerm> Terms { get; set; }

        public KnowledgeBaseVariable(string name, List<KnowledgeBaseTerm> terms)
        {
            Name = name;
            Terms = terms;
            Terms = new List<KnowledgeBaseTerm>();
        }

        public KnowledgeBaseVariable()
        {
            Terms = new List<KnowledgeBaseTerm>();
        }
    }
}
