using FIMAE.ViewModels;
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

namespace FIMAE.Views.NeuralNetwork
{
    /// <summary>
    /// Interaction logic for NetworkView.xaml
    /// </summary>
    public partial class NetworkView : UserControl
    {
        public NetworkView()
        {
            InitializeComponent();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateRelations();
        }

        private void ScrollViewer_MouseEnter(object sender, MouseEventArgs e)
        {
            UpdateRelations();
        }

        private void Nodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRelations();
        }

        private void UpdateRelations()
        {
            var aosList = (DataContext as AOSContainerViewModel).AOSViewModelsList;

            ItemsCanvas.Children.Clear();

            var FeatureNameHeight = 60;
            for (var i = 0; i < aosList.Count - 1; i++)
            {
                UIElement currContainer = (UIElement)Panel.ItemContainerGenerator.ContainerFromIndex(i);
                UIElement nextContainer = (UIElement)Panel.ItemContainerGenerator.ContainerFromIndex(i + 1);

                for (int j = 0; j < aosList[i].FeatureValues.Count; j++)
                {
                    for (int k = 0; k < aosList[i + 1].FeatureValues.Count; k++)
                    {
                        var currHeight = (currContainer.DesiredSize.Height - FeatureNameHeight) / aosList[i].FeatureValues.Count;
                        var currPoint = currContainer.TranslatePoint(
                            new Point(currContainer.DesiredSize.Width - 35,
                                (j + 0.5) * currHeight + FeatureNameHeight), ItemsCanvas);

                        var nextHeight = (nextContainer.DesiredSize.Height - FeatureNameHeight) / aosList[i + 1].FeatureValues.Count;
                        var nextPoint = nextContainer.TranslatePoint(
                            new Point(35,
                                (k + 0.5) * nextHeight + FeatureNameHeight), ItemsCanvas);

                        Line line = new Line()
                        {
                            Stroke = aosList[i].CalculatedValue == aosList[i].FeatureValues[j] && aosList[i + 1].CalculatedValue == aosList[i + 1].FeatureValues[k] ?
                            System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.Black,
                            StrokeThickness = 1,
                            X1 = currPoint.X,
                            Y1 = currPoint.Y,
                            X2 = nextPoint.X,
                            Y2 = nextPoint.Y
                        };

                        ItemsCanvas.Children.Add(line);
                    }
                }
            }
        }
    }
}
