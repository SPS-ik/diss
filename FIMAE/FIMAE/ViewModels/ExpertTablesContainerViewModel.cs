using FIMAE.Helpers;
using FIMAE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FIMAE.ViewModels
{
    public class ExpertTablesContainerViewModel : BaseViewModel
    {        
        private FimasModel _fimas;
        public ObservableCollection<ExpertTableViewModel> ExpertTablesViewModelsList { get; set; }

        public ExpertTablesContainerViewModel(FimasModel fimas)
        {
            _fimas = fimas;
            _fimas.OnExpertTablesChanged += ExpertTablesChanged;

            ExpertTablesViewModelsList = new ObservableCollection<ExpertTableViewModel>();
            foreach (var table in _fimas.GetTables())
            {
                ExpertTablesViewModelsList.Add(new ExpertTableViewModel(table));
            }
        }

        public void ExpertTablesChanged()
        {
            ExpertTablesViewModelsList.Clear();
            foreach (var table in _fimas.GetTables())
            {
                ExpertTablesViewModelsList.Add(new ExpertTableViewModel(table));
            }
            OnPropertyChanged("ExpertTablesViewModelsList");
        }

        public Visibility IsVisible { get; set; }

        public string Header
        {
            get { return (IsVisible == Visibility.Visible ? @"\/" : @"/\") + " Експертні таблиці"; }
        }

        private ICommand _changeVisibilityCommand;
        public ICommand ChangeVisibilityCommand
        {
            get
            {
                return _changeVisibilityCommand ?? (_changeVisibilityCommand = new RelayCommand((obj) => { ChangeVisibility(obj); }));
            }
        }

        public void ChangeVisibility(object o)
        {
            if (IsVisible == Visibility.Visible)
            {
                IsVisible = Visibility.Collapsed;
            }
            else
            {
                IsVisible = Visibility.Visible;
            }
            OnPropertyChanged("Header");
            OnPropertyChanged("IsVisible");
        }        
    }
}
