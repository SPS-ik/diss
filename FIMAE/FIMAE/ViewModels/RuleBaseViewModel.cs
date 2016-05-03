using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS;
using FIMAE.Models;
using System.Collections.ObjectModel;
using FIMAE.FIMAS.KnowledgeBase;

namespace FIMAE.ViewModels
{
    public class RuleBaseViewModel : BaseViewModel
    {
        private FimasModel _fimas;

        public RuleBaseViewModel(FimasModel fimas)
        {
            _fimas = fimas;
            _fimas.OnRuleBaseChanged += RuleBaseChanged;
            _fimas.OnLimitsChanged += LimitsChanged;

            RulesList = new ObservableCollection<KnowledgeBaseRule>();
        }

        public string Header
        {
            get
            {
                string result = String.Format("Rule base contains {0} limit(s) and {1} rule(s)", _fimas.GetLimits().Count, RulesList.Count);
                return result;
            }
        }

        public ObservableCollection<KnowledgeBaseRule> RulesList
        {
            get
            {
                var rules = new ObservableCollection<KnowledgeBaseRule>(_fimas.GetRules());
                return rules;
            }
            set { }
        }

        public void RuleBaseChanged()
        {
            OnPropertyChanged("RulesList");
            OnPropertyChanged("Header");
        }

        public void LimitsChanged()
        {
            OnPropertyChanged("Header");
        }
    }
}
