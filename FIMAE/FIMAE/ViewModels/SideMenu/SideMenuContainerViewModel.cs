using FIMAE.FIMAS;
using FIMAE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIMAE.ViewModels.SideMenu
{
    public class SideMenuContainerViewModel : BaseViewModel
    {
        private FimasModel _fimas;

        public SideMenuContainerViewModel(FimasModel fimas)
        {
            _fimas = fimas;
            CurrentViewModel = new SideMenuStartViewModel(this, _fimas);
        }


        private ISideMenuViewModel _currentViewModel;
        public ISideMenuViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set 
            { 
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

    }
}