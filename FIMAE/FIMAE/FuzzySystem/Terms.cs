using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FIMAE.FuzzySystem
{
    public enum TermTypes
    {
        Trapezoid = 0,
		Sigmoid	= 1
    }
	public enum FuzzyImplicationType
	{
		Larsen = 0,
        Mamdami = 1
	}
       
    abstract public class Term
    {
        public string Name;
        public TermTypes Type;
		public FuzzyImplicationType ImplicationType;

		public double A = 0.0;
		public double B = 0.0;
		public double C = 0.0;
		public double D = 0.0;

        public double AlfaLevel = 0.0;

        public Term (string name, double a, double b, double c, double d)
        {
            Name = name;
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public abstract double F(double var);

        public virtual double GetMin()
        {
            return A;
        }

        public virtual double GetMax()
        {
            return D;
        }

        public double alfaFilter(double var)
        {
            if (ImplicationType == FuzzyImplicationType.Larsen) 
				return ( this.F(var) * AlfaLevel );
			else
				if (this.F(var) > AlfaLevel)
					return AlfaLevel;
            
            return this.F(var);
        }
    }

    public class TrapezoidTerm : Term
    {
        public TrapezoidTerm(string name, double a, double b, double c, double d):
            base(name, a, b, c, d)
        {
            base.Type = TermTypes.Trapezoid;
        }

        public override double F(double var)
        {            
            if (var >= B && var <= C)
                return 1.0;
            if (var > A && var < B)
				return (var - A) / (B - A);
			if (var > C && var < D)
				return (var - D) / (C - D);
                      
            return 0.0;
        }
    }

	public class SigmoidTerm : Term
	{  
		public SigmoidTerm(string name, double a, double b, double c, double d):
            base(name, a, b, c, d)
        {
            base.Type = TermTypes.Sigmoid;
        }

		private double S(double a, double b,double var)
		{
			double c = (a + b) / 2;

			double p1 = (var - a) * (var - a) / (2 * (c - a) * (c - a));
			double p2 = 1.0 - (b - var) * (b - var) / (2 * (c - a) * (c - a));

			if ( b >= a)
			{
				if (var <= c) return p1;
				if (var > c) return p2;
			}
			if (b < a)
			{
				if (var <= c) return p2;
				if (var > c) return p1;				
			}

			return 0.0;
		}

		public override double F(double var)
		{
			if (var >= B && var <= C)
                return 1.0;
			if (A < var && var < B)
				return S(A, B, var);
			if (C < var && var < D)
				return S(D, C, var);

			return 0.0;
		}
	}
}
