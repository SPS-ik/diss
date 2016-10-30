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
            _fimas.AosList.Add(new AgentOrientedSubsystem(definingFeature,
                _fimas.ExpertSystemAgent,_fimas.FuzzyAgent));
                                    
            if (OnAosListChanged != null)
            {
                OnAosListChanged();
            }

            if (_fimas.AosList.Count > 1)
            {
                AddTable(
                    new ExpertSystemTable(
                        _fimas.AosList[_fimas.AosList.Count - 2].CurrentFeature,
                        _fimas.AosList[_fimas.AosList.Count - 1].CurrentFeature));

                if (OnExpertTablesChanged != null)
                {
                    OnExpertTablesChanged();
                }
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
            
            AddTable(
                new ExpertSystemTable(
                    _fimas.LimitsList[_fimas.LimitsList.Count - 1],
                    _fimas.LimitsList[_fimas.LimitsList.Count - 1].DependedFeature));

            if (OnExpertTablesChanged != null)
            {
                OnExpertTablesChanged();
            }
        }

        public List<ExpertSystemTable> GetTables()
        {
            return _fimas.ExpertSystemAgent.ExpertSystemTables;
        }

        public void AddTable(ExpertSystemTable table)
        {
            _fimas.ExpertSystemAgent.ExpertSystemTables.Add(table);

            if (OnExpertTablesChanged != null)
            {
                OnExpertTablesChanged();
            }
        }

        public List<KnowledgeBaseRule> GetRules()
        {
            //return _fimas.KnowledgeBaseAgent.GetRules();
            throw new NotImplementedException();
        }

        public void AddRule(KnowledgeBaseRule rule)
        {
            //_fimas.KnowledgeBaseAgent.AddRule(rule);

            //if (OnRuleBaseChanged != null)
            //{
            //    OnRuleBaseChanged();
            //}
            throw new NotImplementedException();
        }

        public void Calculate(List<ExpertExpression> inputExpressions, string inputValue)
        {
            _fimas.Calculate(inputExpressions, inputValue);
        }
       

    }
}
