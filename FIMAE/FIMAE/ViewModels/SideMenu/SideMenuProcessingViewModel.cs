using FIMAE.FIMAS;
using FIMAE.FIMAS.ExpertSystem;
using FIMAE.FIMAS.Limits;
using FIMAE.Helpers;
using FIMAE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<ExpertExpression> _limitExpressionsList = new ObservableCollection<ExpertExpression>();

        public SideMenuProcessingViewModel(SideMenuContainerViewModel container, FimasModel fimas)
        {
            _fimas = fimas;
            _fimas.OnAosListChanged += AOSListChanged;
            _fimas.OnLimitsChanged += LimitsChanged;
            _container = container;
        }

        public void AOSListChanged()
        {
            OnPropertyChanged("InputValuesList");
        }

        public void LimitsChanged()
        {
            OnPropertyChanged("LimitsList");
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


        public ObservableCollection<string> InputValuesList
        {
            get
            {
                if (_fimas.GetAosList().Count > 0)
                {
                    return new ObservableCollection<string>(_fimas.GetAosList()[0].CurrentFeature.Values); 
                }
                return null;
            }
        }

        private string _inputValue;
        public string InputValue
        {
            get { return _inputValue; }
            set
            {
                _inputValue = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        public ObservableCollection<Limit> LimitsList
        {
            get
            {
                var limits = new ObservableCollection<Limit>(_fimas.GetLimits());

                ObservableCollection<Limit> result = new ObservableCollection<Limit>();
                foreach (var limit in limits)
                {
                    var isAccessible = true;
                    foreach (var inExpr in LimitExpressionsList)
                    {
                        if (inExpr.Variable.Name == limit.Name)
                        {
                            isAccessible = false;
                        }
                    }
                    if (isAccessible)
                    {
                        result.Add(limit);
                    }
                }

                return result;
            }
        }

        private Limit _selectedLimit;
        public Limit SelectedLimit
        {
            get { return _selectedLimit; }
            set
            {
                _selectedLimit = value;
                OnPropertyChanged("SelectedLimitValuesList");
                RelayCommand.UpdateCanExecute();
            }
        }

        public ObservableCollection<string> SelectedLimitValuesList
        {
            get
            {
                if (SelectedLimit != null)
                {
                    return new ObservableCollection<string>(SelectedLimit.Values);
                }
                return null;
            }
        }

        private string _selectedLimitValue;
        public string SelectedLimitValue
        {
            get { return _selectedLimitValue; }
            set
            {
                _selectedLimitValue = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        public ObservableCollection<ExpertExpression> LimitExpressionsList
        {
            get { return _limitExpressionsList; }
        }


        // AddLimitCommand
        private ICommand _addLimitCommand;
        public ICommand AddLimitCommand
        {
            get
            {
                return _addLimitCommand ?? (_addLimitCommand = new RelayCommand(
                    (obj) =>
                    {
                        AddLimit(obj);
                    },
                    o =>
                    {
                        return SelectedLimit != null && !string.IsNullOrEmpty(SelectedLimitValue);
                    }));
            }
        }

        public void AddLimit(object o)
        {
            _limitExpressionsList.Add(new ExpertExpression(SelectedLimit, SelectedLimitValue));
            SelectedLimit = null;
            OnPropertyChanged("SelectedLimit");

            SelectedLimitValue = "";
            OnPropertyChanged("SelectedLimitValue");
            OnPropertyChanged("LimitsList");            
        }

        // CalculateCommand
        private ICommand _calculateCommand;
        public ICommand CalculateCommand
        {
            get
            {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand(
                    (obj) =>
                    {
                        Calculate(obj);
                    },
                    o =>
                    {
                        return !string.IsNullOrEmpty(InputValue);
                    }));
            }
        }

        public void Calculate(object o)
        {
            _fimas.Calculate(LimitExpressionsList.ToList(), InputValue);
        }
    }
}
