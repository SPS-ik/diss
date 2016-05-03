using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySystem.Rules
{
    public class Rule
    {
        public List<Expression> Conditions = new List<Expression>();
        public List<Expression> Results = new List<Expression>();
        public double Weight = 1.0;

        private InferenceMethodDelegate Inference = FuzzyMath.Min;

        public void CreateCondition(FuzzyVariable v, Term t)
        {
            Conditions.Add(new Expression(v, t));
        }

        public void RemoveCondition(FuzzyVariable v)
        {
            for (int i = 0; i < Conditions.Count; i++)
                if (Conditions[i].Variable == v)
                {
                    Conditions.RemoveAt(i);
                    break;
                }
        }

        public void CreateResult(FuzzyVariable v, Term t)
        {
            Results.Add(new Expression(v, t));
        }

        public double AlfaLevelOfCondition()
        {
            List<double> alfaLevelsOfConditions = new List<double>();

            for (int i = 0; i < Conditions.Count; i++)
                if (Conditions[i].Term != null)
                    alfaLevelsOfConditions.Add(Conditions[i].Term.AlfaLevel);

            if (alfaLevelsOfConditions.Count == 0) return 0;

            return Inference(alfaLevelsOfConditions);
        }
    }
}
