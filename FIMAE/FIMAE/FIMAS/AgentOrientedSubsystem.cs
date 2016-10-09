using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.Helpers;
using FIMAE.FIMAS.ExpertSystem;
using System.Xml.Serialization;
using FIMAE.FIMAS.FuzzySystem;

namespace FIMAE.FIMAS
{
    [Serializable]
    public class AgentOrientedSubsystem
    {
        DefiningFeature _currentFeature;
        ExpertSystemAgent _expertSystemAgent;
        FuzzyAgent _fuzzyAgent;
        string _calculatedValue;

        [NonSerialized]
        [XmlIgnore]
        public Action OnAosSelectionChanged;

        public AgentOrientedSubsystem()
        {
        }

        public AgentOrientedSubsystem(DefiningFeature currentFeature, ExpertSystemAgent expertSystemAgent,
            FuzzyAgent fuzzyAgent)
        {
            _currentFeature = currentFeature;
            _expertSystemAgent = expertSystemAgent;
            _fuzzyAgent = fuzzyAgent;
        }

        public DefiningFeature CurrentFeature
        { 
            get { return _currentFeature; }
            set { _currentFeature = value; }
        }

        public string CalculatedValue
        {
            get { return _calculatedValue; }
            set
            {
                _calculatedValue = value;
                if (OnAosSelectionChanged != null)
                {
                    OnAosSelectionChanged();
                }
            }
        }

        DefiningFeatureValue Process(DefiningFeatureValue _currentFeatureValue)
        {
            return null;
        }

        public string Calculate(List<ExpertExpression> inputExpressions)
        {
            return CalculatedValue = _expertSystemAgent.CalculateOutputValue(inputExpressions, _currentFeature);
        }

        public override string ToString()
        {
            return _currentFeature.Name;
        }
    }
}
