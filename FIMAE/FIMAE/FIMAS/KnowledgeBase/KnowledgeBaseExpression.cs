using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.KnowledgeBase
{
    public class KnowledgeBaseExpression
    {
        public KnowledgeBaseVariable Variable { get; set; }
        public KnowledgeBaseTerm Term { get; set; }

        public KnowledgeBaseExpression(KnowledgeBaseVariable v, KnowledgeBaseTerm t)
        {
            Variable = v;
            Term = t;
        }
    }
}
