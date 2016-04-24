using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FIMAS;
using FIMAE.FuzzySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS.KnowledgeBase;

namespace FIMAE.Models
{
    public class FimasModel
    {
        public Fimas _fimas = new Fimas();

        public Action OnAosListChanged;
        public Action OnLimitsChanged;
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
        }

        public void AddLimit(KnowledgeBaseVariable variable)
        {
            _fimas.KnowledgeBaseController.AddVariable(variable);

            if (OnLimitsChanged != null)
            {
                OnLimitsChanged();
            }
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
