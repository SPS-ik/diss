using FIMAE.FuzzySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.KnowledgeBase
{
    public class KnowledgeBaseVariable
    {
        public string Name;
        public List<KnowledgeBaseTerm> Terms = new List<KnowledgeBaseTerm>();

    }
}
