using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.KnowledgeBase
{
    public class KnowledgeBaseExpression
    {
        public KnowledgeBaseVariable Variable;
        public KnowledgeBaseTerm Term;

        public KnowledgeBaseExpression(KnowledgeBaseVariable v, KnowledgeBaseTerm t)
        {
            Variable = v;
            Term = t;
        }
    }
}
