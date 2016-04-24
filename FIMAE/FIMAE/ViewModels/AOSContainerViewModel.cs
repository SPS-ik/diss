using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS;
using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.Models;
using System.Collections.ObjectModel;

namespace FIMAE.ViewModels
{
    public class AOSContainerViewModel : BaseViewModel
    {
        private FimasModel _fimas;
        public ObservableCollection<AOSViewModel> AOSViewModelsList { get; set; }

        public AOSContainerViewModel(FimasModel fimas)
        {
            _fimas = fimas;
            _fimas.OnAosListChanged += AOSListChanged;

            AOSViewModelsList = new ObservableCollection<AOSViewModel>();
            foreach (var aos in _fimas.GetAosList())
            {
                AOSViewModelsList.Add(new AOSViewModel(aos));
            }
        }

        public void AOSListChanged()
        {
            AOSViewModelsList.Clear();
            foreach (var aos in _fimas.GetAosList())
            {
                AOSViewModelsList.Add(new AOSViewModel(aos));
            }
            OnPropertyChanged("AOSViewModelsList");
        }
    }
}
