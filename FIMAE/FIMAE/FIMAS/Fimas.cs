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
        public List<Limit> LimitsList = new List<Limit>();
        public ExpertSystemController ExpertSystemController = new ExpertSystemController();

        public void Calculate(List<ExpertExpression> inputExpressions, string inputValue)
        {
            AosList[0].CalculatedValue = inputValue;

            inputExpressions.Add(new ExpertExpression(AosList[0].CurrentFeature, inputValue));
            string currValue;
            for (int i = 1; i < AosList.Count; i++)
            {
                currValue = AosList[i].Calculate(inputExpressions);
                inputExpressions.Add(new ExpertExpression(AosList[i].CurrentFeature, currValue));
            }
        }
    }
}
