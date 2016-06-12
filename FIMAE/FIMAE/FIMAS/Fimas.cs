using FIMAE.FIMAS.DefiningFeatures;
using FIMAE.FIMAS.ExpertSystem;
using FIMAE.FIMAS.Limits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS
{
    [Serializable]
    public class Fimas
    {
        public List<AgentOrientedSubsystem> AosList = new List<AgentOrientedSubsystem>();
        public StepwiseFeaturesModel StepwiseFeaturesModel = new StepwiseFeaturesModel();
        public List<Limit> LimitsList = new List<Limit>();
        public KnowledgeBaseController KnowledgeBaseController = new KnowledgeBaseController();
        public ExpertSystemController ExpertSystemController = new ExpertSystemController();
    }
}
