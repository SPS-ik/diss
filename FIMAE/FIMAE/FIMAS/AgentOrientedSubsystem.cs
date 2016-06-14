using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.Helpers;
using FIMAE.FIMAS.ExpertSystem;

namespace FIMAE.FIMAS
{
    [Serializable]
    public class AgentOrientedSubsystem
    {
        DefiningFeature _currentFeature;
        ExpertSystemController _expertSystemController;
        string _calculatedValue;

        [NonSerialized]
        public Action OnAosSelectionChanged;

        public AgentOrientedSubsystem(DefiningFeature currentFeature, ExpertSystemController expertSystemController)
        {
            _currentFeature = currentFeature;
            _expertSystemController = expertSystemController;
        }

        public DefiningFeature CurrentFeature
        { 
            get { return _currentFeature; } 
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
            return CalculatedValue = _expertSystemController.CalculateOutputValue(inputExpressions, _currentFeature);
        }

        public override string ToString()
        {
            return _currentFeature.Name;
        }
    }
}
