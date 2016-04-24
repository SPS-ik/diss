using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FuzzySystem;

namespace FIMAE.FIMAS
{
    public class AgentOrientedSubsystem
    {
        DefiningFeature _currentFeature;
        StepwiseFeaturesModel _featuresModel;
        KnowledgeBaseController _knowledgeBaseController;

        public AgentOrientedSubsystem(DefiningFeature currentFeature)//, StepwiseFeaturesModel featuresModel, FuzzyController knowledgeBaseController)
        {
            _currentFeature = currentFeature;
           // _featuresModel = featuresModel;
           // _knowledgeBaseController = knowledgeBaseController;
        }

        public DefiningFeature CurrentFeature
        { 
            get { return _currentFeature; } 
        }

        DefiningFeatureValue Process(DefiningFeatureValue _currentFeatureValue)
        {
            return null;
        }
    }
}
