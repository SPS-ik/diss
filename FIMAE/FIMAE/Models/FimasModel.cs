using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FIMAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS.KnowledgeBase;
using FIMAE.FIMAS.Limits;
using FIMAE.FIMAS.ExpertSystem;
using FIMAE.Helpers;

namespace FIMAE.Models
{
    [Serializable]
    public class FimasModel
    {
        public Fimas _fimas = new Fimas();

        public Action OnAosListChanged;
        public Action OnLimitsChanged;
        public Action OnExpertTablesChanged;
        public Action OnRuleBaseChanged;

        public List<AgentOrientedSubsystem> GetAosList()
        {
            return _fimas.AosList;
        }

        public void AddDefiningFeature(DefiningFeature definingFeature)
        {
            _fimas.StepwiseFeaturesModel.DefiningFeaturesList.Add(definingFeature);
            _fimas.AosList.Add(new AgentOrientedSubsystem(definingFeature));
                                    
            if (OnAosListChanged != null)
            {
                OnAosListChanged();
            }

            if (_fimas.AosList.Count > 1)
            {
                AddTable(
                    new ExpertSystemTable(
                        _fimas.AosList[_fimas.AosList.Count - 1].CurrentFeature,
                        _fimas.AosList[_fimas.AosList.Count - 2].CurrentFeature));
                
                OnExpertTablesChanged();
            }
        }

        public List<Limit> GetLimits()
        {
            return _fimas.LimitsList;
        }

        public void AddLimit(Limit variable)
        {
            _fimas.LimitsList.Add(variable);

            if (OnLimitsChanged != null)
            {
                OnLimitsChanged();
            }
        }

        public List<ExpertSystemTable> GetTables()
        {
            return _fimas.ExpertSystemController.ExpertSystemTables;
        }

        public void AddTable(ExpertSystemTable table)
        {
            _fimas.ExpertSystemController.ExpertSystemTables.Add(table);

            if (OnRuleBaseChanged != null)
            {
                OnRuleBaseChanged();
            }
        }

        public List<KnowledgeBaseRule> GetRules()
        {
            return _fimas.KnowledgeBaseController.GetRules();
        }

        public void AddRule(KnowledgeBaseRule rule)
        {
            _fimas.KnowledgeBaseController.AddRule(rule);

            if (OnRuleBaseChanged != null)
            {
                OnRuleBaseChanged();
            }
        }
       

    }
}
