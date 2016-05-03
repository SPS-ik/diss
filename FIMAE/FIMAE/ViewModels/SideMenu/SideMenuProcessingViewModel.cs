using FIMAE.FIMAS;
using FIMAE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIMAE.ViewModels.SideMenu
{
    public class SideMenuProcessingViewModel : BaseViewModel, ISideMenuViewModel
    {
        FimasModel _fimas;
        SideMenuContainerViewModel _container;

        public SideMenuProcessingViewModel(SideMenuContainerViewModel container, FimasModel fimas)
        {
            _fimas = fimas;
            _container = container;
        }

        // BackCommand
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new CommandHandler((o) => Back(o)));
            }
        }

        public void Back(object o)
        {
            _container.CurrentViewModel = new SideMenuStartViewModel(_container, _fimas);
        }
    }
}
