using FIMAE.FuzzySystem.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.KnowledgeBase
{
    public class KnowledgeBaseRule
    {
        public List<KnowledgeBaseExpression> Conditions = new List<KnowledgeBaseExpression>();
        public KnowledgeBaseExpression Result;
    }
}
