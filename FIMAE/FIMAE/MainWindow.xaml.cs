using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FuzzySystem;
using FIMAE.Helpers;

namespace FIMAE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        FuzzyVariable calculatingVariable;

        public MainWindow()
        {
            DataContext = new MainVindowViewModel();
            InitializeComponent();

            var fuzzyController = new FuzzyController();
            var reader = new FSReader(fuzzyController);
            reader.Read(@"D:\!SPS-ik\Studying\Аспирантура\Исходники\NeuroFuzzyConstructor\WindowsApplication1\bin\Debug\ex.fs");


            List<FuzzyElementBase> inVariables = new List<FuzzyElementBase>();
            List<FuzzyElementBase> outVariables = new List<FuzzyElementBase>();
            for (int i = 0; i < fuzzyController.Elements.Count; i++)
            {
                var e = fuzzyController.Elements[i] as FuzzyElementBase;
                if (e.Type == ElementType.Variable)
                {
                    if (e.InLinks.Count == 0 && e.OutLinks.Count != 0)
                    {
                        inVariables.Add(e);
                    }

                    if (e.OutLinks.Count == 0 && e.InLinks.Count != 0)
                    {
                        outVariables.Add(e);
                    }
                }
            }

            inVariables.ForEach(v => (v as FuzzyVariable).ClearValue = 16);

            outVariables.ForEach(v => v.Calculate());

            double min = (outVariables[0] as FuzzyVariable).GetMin();
            double max = (outVariables[0] as FuzzyVariable).GetMax();

			double numerator;
			double denominator;
			for (int i = 0; i < outVariables.Count; i++)
			{
				calculatingVariable = outVariables[i] as FuzzyVariable;
				numerator = FuzzyMath.IntegralSimpson(min, max, 0.0001, MulFunction);
                denominator = FuzzyMath.IntegralSimpson(min, max, 0.0001, calculatingVariable.TermsAlfaLevel);

				if (denominator != 0 && !(denominator == double.NaN && numerator == double.NaN))
					calculatingVariable.ClearValue = numerator / denominator;		
				
			}

        }

        private double MulFunction(double x)
        {
            return (x * calculatingVariable.TermsAlfaLevel(x));
        }
    }
}
