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
        }

        private double MulFunction(double x)
        {
            return (x * calculatingVariable.TermsAlfaLevel(x));
        }
    }
}
