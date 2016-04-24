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
    public class SideMenuStartViewModel : BaseViewModel, ISideMenuViewModel
    {
        private FimasModel _fimas;
        private SideMenuContainerViewModel _container;

        public SideMenuStartViewModel(SideMenuContainerViewModel container, FimasModel fimas)
        {
            _fimas = fimas;
            _container = container;
        }
                
        // OpenConstructorCommand
        private ICommand _openConstructorCommand;
        public ICommand OpenConstructorCommand
        {
            get
            {
                return _openConstructorCommand ?? (_openConstructorCommand = new CommandHandler((o) => OpenConstructor(o), _canOpenConstructor));
            }
        }

        private bool _canOpenConstructor = true;
        public void OpenConstructor(object o)
        {
            _container.CurrentViewModel = new SideMenuConstructorViewModel(_container, _fimas);
        }


        // OpenProcessindCommand
        private ICommand _openProcessindCommand;
        public ICommand OpenProcessindCommand
        {
            get
            {
                return _openProcessindCommand ?? (_openProcessindCommand = new CommandHandler((o) => OpenProcessind(o), _canOpenProcessing));
            }
        }

        private bool _canOpenProcessing = true;
        public void OpenProcessind(object o)
        {
            _container.CurrentViewModel = new SideMenuProcessingViewModel(_container, _fimas);
        }
    }
}
