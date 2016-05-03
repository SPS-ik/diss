using FuzzySystem.Rules;
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

        public override string ToString()
        {
            string result = "If ";
            foreach(var cond in Conditions)
            {
                result += String.Format("{0} = {1}", cond.Variable.Name, cond.Term.Name);
                if (Conditions.IndexOf(cond) < Conditions.Count - 1)
                {
                    result += " and ";
                }
                else
                {
                    result += " then ";
                }
            }
            if (Result != null)
            {
                result += String.Format("{0} = {1}", Result.Variable.Name, Result.Term.Name);
            }

            return result;
        }
    }
}
