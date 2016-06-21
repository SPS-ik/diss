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
using FIMAE.Helpers;
using FIMAE.FIMAS.Limits;
using System.Windows.Forms;

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
        private Serializer saver = new Serializer();

        public SideMenuConstructorViewModel(SideMenuContainerViewModel container, FimasModel fimas)
        {
            _fimas = fimas;
            _fimas.OnAosListChanged += AOSListChanged;

            _container = container;
            NewPropertyValues = new ObservableCollection<string>();
            NewLimitTerms = new ObservableCollection<string>();
            AddedInputExpressions = new ObservableCollection<KnowledgeBaseExpression>();
        }

        public void AOSListChanged()
        {
            OnPropertyChanged("AosList");
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
                OnPropertyChanged("PropertiesHeader");
                OnPropertyChanged("LimitsHeader");
                OnPropertyChanged("RulesHeader");                
            }
        }


        
        // ChangeModeCommand
        private ICommand _changeModeCommand;
        public ICommand ChangeModeCommand
        {
            get
            {
                return _changeModeCommand ?? (_changeModeCommand = new RelayCommand((obj) => { ChangeMode(obj); }));
            }
        }

        public void ChangeMode(object o)
        {
            CurrentMode = (string)o;
        }

        // SaveCommand
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand((o) => Save(o)));
            }
        }

        public void Save(object o)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files|*.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saver.SaveToXml(_fimas._fimas, saveFileDialog.FileName);
            }
        }

        // OpenCommand
        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                return _openCommand ?? (_openCommand = new RelayCommand((o) => Open(o)));
            }
        }

        public void Open(object o)
        {
            Fimas restoredFimas;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files|*.xml";
            openFileDialog.DefaultExt = "xml";
            openFileDialog.AddExtension = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                restoredFimas = saver.RestoreFromXml(openFileDialog.FileName);

                _fimas.GetAosList().Clear();
                foreach (var aos in restoredFimas.AosList)
                {
                    _fimas.AddDefiningFeature(aos.CurrentFeature);
                }

                _fimas.GetLimits().Clear();
                foreach (var limit in restoredFimas.LimitsList)
                {
                    _fimas.AddLimit(limit);
                }

                _fimas.GetTables().Clear();
                foreach (var table in restoredFimas.ExpertSystemController.ExpertSystemTables)
                {
                    _fimas.AddTable(table);
                }
            }
        }


        // BackCommand
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand((o) => Back(o)));
            }
        }

        public void Back(object o)
        {
            _container.CurrentViewModel = new SideMenuStartViewModel(_container, _fimas);
        }

        
        #region Properties
        public string PropertiesHeader
        {
            get
            {
                return String.Format("{0} Додати ознаку", _currentMode == FimaeConstructorModes.PropertiesState ? @"/\" : @"\/");
            }
        }

        private string _newPropertyName;
        public string NewPropertyName 
        {
            get {return _newPropertyName;}
            set
            {
                _newPropertyName = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        private string _newPropertyValue;
        public string NewPropertyValue
        {
            get { return _newPropertyValue; }
            set
            {
                _newPropertyValue = value;
                RelayCommand.UpdateCanExecute();
            }
        }
        public ObservableCollection<string> NewPropertyValues { get; set; }

        private ICommand _addPropertyValueCommand;
        public ICommand AddPropertyValueCommand
        {
            get
            {
                return _addPropertyValueCommand ?? (_addPropertyValueCommand = new RelayCommand(
                    (obj) => 
                    { 
                        AddPropertyValue(obj);
                    },
                    o =>
                    { 
                        return !String.IsNullOrEmpty(NewPropertyValue);
                    }));
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
                return _addPropertyCommand ?? (_addPropertyCommand = new RelayCommand(
                    (obj) =>
                    {
                        AddProperty(obj);
                    },
                    o =>
                    {
                        return !String.IsNullOrEmpty(NewPropertyName) && NewPropertyValues.Count > 0;
                    }));
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
        #endregion

        #region Limits
        public string LimitsHeader
        {
            get
            {
                return String.Format("{0} Додати обмеження", _currentMode == FimaeConstructorModes.LimitsState ? @"/\" : @"\/");
            }
        }

        public ObservableCollection<AgentOrientedSubsystem> AosList
        {
            get { return new ObservableCollection<AgentOrientedSubsystem>(_fimas.GetAosList().Where(n => _fimas.GetAosList().IndexOf(n) != 0).ToList()); }
        }

        private AgentOrientedSubsystem _dependedAos;
        public AgentOrientedSubsystem DependedAos
        {
            get { return _dependedAos; }
            set
            {
                _dependedAos = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        private string _newLimitName;
        public string NewLimitName
        {
            get { return _newLimitName; }
            set 
            {
                _newLimitName = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        private string _newLimitTermName;
        public string NewLimitTermName
        {
            get { return _newLimitTermName; }
            set
            {
                _newLimitTermName = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        private double _a;
        public double A
        {
            get { return _a; }
            set
            {
                _a = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        private double _b;
        public double B
        {
            get { return _b; }
            set
            {
                _b = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        private double _c;
        public double C
        {
            get { return _c; }
            set
            {
                _c = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        private double _d;
        public double D
        {
            get { return _d; }
            set
            {
                _d = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        public ObservableCollection<string> NewLimitTerms { get; set; }        

        // AddLimitTermCommand
        private ICommand _addLimitTermCommand;
        public ICommand AddLimitTermCommand
        {
            get
            {
                return _addLimitTermCommand ?? (_addLimitTermCommand = new RelayCommand(
                    (obj) =>
                    {
                        AddLimitTerm(obj);
                    },
                    o =>
                    {
                        return !String.IsNullOrEmpty(NewLimitTermName);
                    }));
            }
        }

        public void AddLimitTerm(object o)
        {
            NewLimitTerms.Add(NewLimitTermName);
            OnPropertyChanged("NewLimitTerms");

            NewLimitTermName = "";
            OnPropertyChanged("NewLimitTermName");
            A = 0;
            OnPropertyChanged("A");
            B = 0;
            OnPropertyChanged("B");
            C = 0;
            OnPropertyChanged("C");
            D = 0;
            OnPropertyChanged("D");
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
                        return DependedAos != null && !String.IsNullOrEmpty(NewLimitName) && NewLimitTerms.Count > 0;
                    }
                    ));
            }
        }

        public void AddLimit(object o)
        {
            var newVariable = new Limit();
            newVariable.Name = NewLimitName + " (Limit)";
            newVariable.DependedFeature = DependedAos.CurrentFeature;
            foreach (var value in NewLimitTerms)
            {
                newVariable.Values.Add(value);
            }

            _fimas.AddLimit(newVariable);

            NewLimitName = "";
            OnPropertyChanged("NewLimitName");

            NewLimitTermName = "";
            OnPropertyChanged("NewLimitTermName");

            NewLimitTerms.Clear();
            OnPropertyChanged("NewLimitTerms");

            OnPropertyChanged("AccessibleInVars");
            OnPropertyChanged("AccessibleOutVars");

            DependedAos = null;
            OnPropertyChanged("DependedAos");
        }
        #endregion
        
        #region Rules
        public string RulesHeader
        {
            get
            {
                return String.Format("{0} Додати правило", _currentMode == FimaeConstructorModes.RulesState ? @"/\" : @"\/");
            }
        }
        public ObservableCollection<Limit> AccessibleInVars 
        {
            get 
            {
                var limits = new ObservableCollection<Limit>(_fimas.GetLimits());

                ObservableCollection<Limit> result = new ObservableCollection<Limit>();
                foreach(var limit in limits)
                {
                    var isAccessible = true;
                    foreach(var inExpr in AddedInputExpressions)
                    {
                        if (inExpr.Variable.Name == limit.Name)
                        {
                            isAccessible = false;
                        }
                    }
                    if ((SelectedOutputVar != null) && (SelectedOutputVar.Name == limit.Name))
                    {
                        isAccessible = false;
                    }

                    if(isAccessible)
                    {
                        result.Add(limit);
                    }
                }
                
                return result;
            }
            set {}
        }
        
        // input
        public ObservableCollection<KnowledgeBaseExpression> AddedInputExpressions { get; set; }

        private KnowledgeBaseVariable _selectedInputVar;
        public KnowledgeBaseVariable SelectedInputVar
        { 
            get {return _selectedInputVar;}
            set
            {
                _selectedInputVar = value;
                OnPropertyChanged("AccessibleInputTerms");
                OnPropertyChanged("SelectedInputVar");

                if (SelectedOutputVar != null)
                {
                    _selectedOutputVar = null;
                    OnPropertyChanged("SelectedOutputVar");
                    _selectedOutputTerm = null;
                    OnPropertyChanged("SelectedOutputTerm");
                    AccessibleOutputTerms.Clear();
                    OnPropertyChanged("AccessibleOutputTerms");
                }
       
                RelayCommand.UpdateCanExecute();
            }
        }

        public ObservableCollection<KnowledgeBaseTerm> AccessibleInputTerms
        {
            get
            {
                var terms = new ObservableCollection<KnowledgeBaseTerm>();
                if (SelectedInputVar != null && SelectedInputVar.Terms != null)
                {
                    SelectedInputVar.Terms.ForEach(terms.Add);
                }
                return terms;
            }
            set { }
        }

        private KnowledgeBaseTerm _selectedInputTerm;
        public KnowledgeBaseTerm SelectedInputTerm
        {
            get { return _selectedInputTerm; }
            set
            {
                _selectedInputTerm = value;
                RelayCommand.UpdateCanExecute();
            }
        }

        // output
        public ObservableCollection<Limit> AccessibleOutVars
        {
            get
            {
                var limits = new ObservableCollection<Limit>(_fimas.GetLimits());

                ObservableCollection<Limit> result = new ObservableCollection<Limit>();
                foreach (var limit in limits)
                {
                    var isAccessible = true;
                    foreach (var inExpr in AddedInputExpressions)
                    {
                        if (inExpr.Variable.Name == limit.Name)
                        {
                            isAccessible = false;
                        }
                    }
                    if ((SelectedInputVar != null) && (SelectedInputVar.Name == limit.Name))
                    {
                        isAccessible = false;
                    }

                    if (isAccessible)
                    {
                        result.Add(limit);
                    }
                }

                return result;
            }
            set { }
        }

        private KnowledgeBaseVariable _selectedOutputVar;
        public KnowledgeBaseVariable SelectedOutputVar
        {
            get { return _selectedOutputVar; }
            set
            {

                _selectedOutputVar = value;
                OnPropertyChanged("AccessibleOutputTerms");
                OnPropertyChanged("SelectedOutputVar");

                if (SelectedInputVar != null)
                {
                    _selectedInputVar = null;
                    OnPropertyChanged("SelectedInputVar");
                    _selectedInputTerm = null;
                    OnPropertyChanged("SelectedInputTerm");
                    AccessibleInputTerms.Clear();
                    OnPropertyChanged("AccessibleInputTerms");
                }

                RelayCommand.UpdateCanExecute();
            }
        }

        public ObservableCollection<KnowledgeBaseTerm> AccessibleOutputTerms
        {
            get
            {
                var terms = new ObservableCollection<KnowledgeBaseTerm>();
                if (SelectedOutputVar != null && SelectedOutputVar.Terms != null)
                {
                    SelectedOutputVar.Terms.ForEach(terms.Add);
                }
                return terms;
            }
            set { }
        }

        private KnowledgeBaseTerm _selectedOutputTerm;
        public KnowledgeBaseTerm SelectedOutputTerm
        {
            get { return _selectedOutputTerm; }
            set
            {
                _selectedOutputTerm = value;
                RelayCommand.UpdateCanExecute();
            }
        }


        // AddInputExpressionCommand
        private ICommand _addInputExpressionCommand;
        public ICommand AddInputExpressionCommand
        {
            get
            {
                return _addInputExpressionCommand ?? (_addInputExpressionCommand = new RelayCommand(
                    (obj) =>
                    {
                        AddInputExpression(obj);
                    },
                    o =>
                    {
                        return SelectedInputVar != null && SelectedInputTerm != null;
                    }));
            }
        }

        public void AddInputExpression(object o)
        {
            AddedInputExpressions.Add(new KnowledgeBaseExpression(SelectedInputVar, SelectedInputTerm));

            SelectedInputVar = null;
            OnPropertyChanged("SelectedInputVar");

            SelectedInputTerm = null;
            OnPropertyChanged("SelectedInputTerm");

            OnPropertyChanged("AccessibleInVars");
            OnPropertyChanged("AccessibleOutVars");    
        }



        // AddRuleCommand
        private ICommand _addRuleCommand;
        public ICommand AddRuleCommand
        {
            get
            {
                return _addRuleCommand ?? (_addRuleCommand = new RelayCommand(
                    (obj) =>
                    {
                        AddRule(obj);
                    },
                    o =>
                    {
                        return AddedInputExpressions.Count >0 && SelectedOutputVar != null && SelectedOutputTerm != null;
                    }));
            }
        }

        public void AddRule(object o)
        {
            var rule = new KnowledgeBaseRule()
            {
                Conditions = AddedInputExpressions.ToList(),
                Result = new KnowledgeBaseExpression(SelectedOutputVar, SelectedOutputTerm)
            };
            //_fimas.AddRule(rule);

            AddedInputExpressions.Clear();
            OnPropertyChanged("AddedInputExpressions");

            SelectedInputVar = null;
            OnPropertyChanged("SelectedInputVar");

            SelectedInputTerm = null;
            OnPropertyChanged("SelectedInputTerm");

            SelectedOutputVar = null;
            OnPropertyChanged("SelectedOutputVar");

            SelectedOutputTerm = null;
            OnPropertyChanged("SelectedOutputTerm");

            OnPropertyChanged("AccessibleInVars");
            OnPropertyChanged("AccessibleOutVars");     
        }

        #endregion
    }
}
