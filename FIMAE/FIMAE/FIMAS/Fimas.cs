using FIMAE.FIMAS.DefiningFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS
{
    public class Fimas
    {
        public List<AgentOrientedSubsystem> AosList = new List<AgentOrientedSubsystem>();
        public StepwiseFeaturesModel StepwiseFeaturesModel = new StepwiseFeaturesModel();
        public KnowledgeBaseController KnowledgeBaseController = new KnowledgeBaseController();
    }
}
