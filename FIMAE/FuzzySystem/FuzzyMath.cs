using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzySystem
{
	public delegate double InferenceMethodDelegate(params object[] par);	
	public delegate double FunctionMethodDelefate(double x);

	public class FuzzyMath
	{
		public static double Min(params object[] par)
		{            
			List<double> d = par[0] as List<double>;            
			double min = d.Count>0 ? d[0] : 0;
			for (int i = 1; i < d.Count; i++)
				if (d[i] < min) min = d[i];			
			return min;
		}

		public static double Max(params object[] par)
		{
                List<double> d = par[0] as List<double>;
                double max = d.Count>0 ? d[0] : 0;
                for (int i = 1; i < d.Count; i++)
                    if (d[i] > max) max = d[i];
                return max;	
		}
		public static double Gamma(params object[] par)
		{
			List<double> d = par[0] as List<double>;
			double gamma = (double) par[1];

			double a = 1.0, b = 1.0;

			for (int i = 0; i < d.Count; i++)
			{
				a *= d[i];
				b *= (1 - d[i]);
			}

			return Math.Pow(a, 1 - gamma) * Math.Pow(1 - b, gamma);
		}

		
		public static double Integral(FunctionMethodDelefate f, double min, double max)
		{
			double eps = (max - min) / 1000 ;

			double x = min;
			double i = 0.0;
			while (x < max)
			{
				i += ( f(x) * eps );
				x += eps; 
			}
			return i;
		}
		
		public static double IntegralSimpson(double a, double b, double epsilon, FunctionMethodDelefate f)
		{
			double h;
			double s;
			double s1;
			double s2;
			double s3;
			double x;

			s2 = 1;
			h = b-a;
			s = f(a)+f(b);
			do
			{
				s3 = s2;
				h = h / 2.0;
				s1 = 0;
				x = a+h;
				do
				{
					s1 = s1 + 2 * f(x);
					x = x + 2 * h;
				}
				while(x < b);
				s = s + s1;
				s2 = ((s+s1)*h) / 3.0;
				x = Math.Abs(s3-s2) / 15.0;
			}
			while(x > epsilon);
			return s2;
		}

	}
}
