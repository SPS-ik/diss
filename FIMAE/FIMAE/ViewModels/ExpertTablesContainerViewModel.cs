using FIMAE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
    }
}
