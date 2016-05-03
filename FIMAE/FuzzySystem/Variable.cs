using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FuzzySystem
{
    public enum DefuzzyficationMethod
    {
        CentrOfMass = 0
    }
    public class FuzzyVariable : FuzzyElementBase
    {
        private InferenceMethodDelegate TermInference = FuzzyMath.Max;

        public List<Term> Terms = new List<Term>();
        public double ClearValue;
        public DefuzzyficationMethod defuzzificationMethod = DefuzzyficationMethod.CentrOfMass;
        
        public FuzzyVariable(): base(ElementType.Variable)
        {
        }

        #region Terms
        public Term AddTerm(string name, TermTypes type, double a, double b, double c, double d)
        {
            if (type == TermTypes.Sigmoid)
            {
                var t = new SigmoidTerm(name, a, b, c, d);
                Terms.Add(t);
                return t;
            }
            if (type == TermTypes.Trapezoid)
            {
                var t = new TrapezoidTerm(name, a, b, c, d);
                Terms.Add(t);
                return t;
            }
            return null;
        }

        public void RemoveTerm(int index)
        {
            Terms.Remove(Terms[index]);
        }

        public void RemoveTerm(string name)
        {
            Terms.Remove(Terms.Find(t=>t.Name==name));
        }

        #endregion

        public override void Calculate()
        {
			for (int i = 0; i < Terms.Count; i++)
				Terms[i].AlfaLevel = 0.0;
			
			base.Calculate();

            if (base.InLinks.Count == 0)
            {
                for (int i = 0; i < Terms.Count; i++)
                {
                    Terms[i].AlfaLevel = Terms[i].F(ClearValue);
                }
            }
        }

        public double TermsAlfaLevel(double var)
		{
			List<double> alfaLevels = new List<double>();

			for (int i = 0; i < Terms.Count; i++)
				alfaLevels.Add(Terms[i].alfaFilter(var));

			return TermInference(alfaLevels);

		}

		#region Min Max
		public double GetMin()
		{
			if (Terms.Count == 0) return 0.0;
			double min = Terms[0].GetMin();
			double m;
			for (int i = 1; i < Terms.Count; i++)
				if ( ( m = Terms[i].GetMin() ) < min) min = m;
			return min;
		}
		public double GetMax()
		{
			if (Terms.Count == 0) return 100.0;
			double max = Terms[0].GetMax();
			double m;
			for (int i = 1; i < Terms.Count; i++)
				if ( (m = Terms[i].GetMax()) > max) max = m;
			return max;
		}
		#endregion
	}

}
