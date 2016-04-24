using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FIMAS;
using FIMAE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FIMAE.FIMAS.KnowledgeBase;
using FIMAE.FuzzySystem;

namespace FIMAE.ViewModels.SideMenu
{
    enum FimaeConstructorModes
    {
        NoneState,
        PropertiesState,
        LimitsState,
        RulesState
    }

    public class SideMenuConstructorViewModel : BaseViewModel, ISideMenuViewModel
    {
        private FimasModel _fimas;
        private SideMenuContainerViewModel _container;
        private FimaeConstructorModes _currentMode;

        public SideMenuConstructorViewModel(SideMenuContainerViewModel container, FimasModel fimas)
        {
            _fimas = fimas;
            _container = container;
            NewPropertyValues = new ObservableCollection<string>();
        }

        public string CurrentMode
        {
            get
            {
                return _currentMode.ToString();
            }
            set
            {
                FimaeConstructorModes newMode;
                if (Enum.TryParse<FimaeConstructorModes>(value, out newMode))
                {
                    if (_currentMode == newMode)
                        _currentMode = FimaeConstructorModes.NoneState;
                    else
                        _currentMode = newMode;
                }
                OnPropertyChanged("CurrentMode");
            }
        }
        
        // ChangeModeCommand
        private ICommand _changeModeCommand;
        public ICommand ChangeModeCommand
        {
            get
            {
                return _changeModeCommand ?? (_changeModeCommand = new CommandHandler((obj) => { ChangeMode(obj); }, true));
            }
        }

        public void ChangeMode(object o)
        {
            CurrentMode = (string)o;
        }
        

        // BackCommand
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new CommandHandler((o) => Back(o), _canBack));
            }
        }

        private bool _canBack = true;
        public void Back(object o)
        {
            _container.CurrentViewModel = new SideMenuStartViewModel(_container, _fimas);
        }



        public string NewPropertyName { get; set; }
        public string NewPropertyValue { get; set; }
        public ObservableCollection<string> NewPropertyValues { get; set; }

        private ICommand _addPropertyValueCommand;
        public ICommand AddPropertyValueCommand
        {
            get
            {
                return _addPropertyValueCommand ?? (_addPropertyValueCommand = new CommandHandler((obj) => { AddPropertyValue(obj); }, true));
            }
        }

        public void AddPropertyValue(object o)
        {
            NewPropertyValues.Add(NewPropertyValue);
            OnPropertyChanged("NewPropertyValues");
            NewPropertyValue = "";
            OnPropertyChanged("NewPropertyValue");
        }

        // AddPropertyCommand
        private ICommand _addPropertyCommand;
        public ICommand AddPropertyCommand
        {
            get
            {
                return _addPropertyCommand ?? (_addPropertyCommand = new CommandHandler((obj) => { AddProperty(obj); }, true));
            }
        }

        public void AddProperty(object o)
        {
            DefiningFeature newFeature = new DefiningFeature();
            newFeature.Name = NewPropertyName;
            newFeature.Values = NewPropertyValues.ToList();

            _fimas.AddDefiningFeature(newFeature);

            NewPropertyName = "";
            OnPropertyChanged("NewPropertyName");
            NewPropertyValue = "";
            OnPropertyChanged("NewPropertyValue");
            NewPropertyValues.Clear();
        }


        public string NewLimitName { get; set; }
        public string NewLimitTermName { get; set; }
        public ObservableCollection<string> NewLimitTermNames { get; set; }

        // AddLimitCommand
        private ICommand _addLimitCommand;
        public ICommand AddLimitCommand
        {
            get
            {
                return _addLimitCommand ?? (_addLimitCommand = new CommandHandler((obj) => { AddLimit(obj); }, true));
            }
        }

        public void AddLimit(object o)
        {
            var newVariable = new KnowledgeBaseVariable();
            newVariable.Name = NewLimitName;
            foreach (var term in NewLimitTermNames)
            {
                newVariable.Terms.Add(new KnowledgeBaseTerm(term,0,0,0,0));
            }

            _fimas.AddLimit(newVariable);

            NewPropertyName = "";
            OnPropertyChanged("NewPropertyName");
            NewPropertyValue = "";
            OnPropertyChanged("NewPropertyValue");
            NewPropertyValues.Clear();
        }

    }
}
