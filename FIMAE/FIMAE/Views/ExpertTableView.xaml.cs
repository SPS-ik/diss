﻿using FIMAE.ViewModels;
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

namespace FIMAE.Views
{
    /// <summary>
    /// Interaction logic for ExpertTablesView.xaml
    /// </summary>
    public partial class ExpertTableView : UserControl
    {
        public ExpertTableView()
        {
            InitializeComponent();
        }

        private void dataGrid2D_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            (DataContext as ExpertTableViewModel).SaveTableCommand.Execute(null);
        }
    }
}
