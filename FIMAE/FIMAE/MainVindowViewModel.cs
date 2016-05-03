using FIMAE.FIMAS;
using FIMAE.Models;
using FIMAE.ViewModels;
using FIMAE.ViewModels.SideMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE
{
    public class MainVindowViewModel
    {
        private FimasModel _fimas = new FimasModel();

        private SideMenuContainerViewModel _sideMenuControl;
        public SideMenuContainerViewModel SideMenuControl
        {
            get { return _sideMenuControl ?? (_sideMenuControl = new SideMenuContainerViewModel(_fimas)); }
        }

        private AOSContainerViewModel _aosContainer;
        public AOSContainerViewModel AOSContainer
        {
            get { return _aosContainer ?? (_aosContainer = new AOSContainerViewModel(_fimas)); }
        }

        private RuleBaseViewModel _ruleBase;
        public RuleBaseViewModel RuleBase
        {
            get { return _ruleBase ?? (_ruleBase = new RuleBaseViewModel(_fimas)); }
        }

        public MainVindowViewModel()
        {
            
        }
    }
}
