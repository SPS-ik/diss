using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.ExpertSystem
{
    [Serializable]
    public class ExpertSystemController
    {
        public List<ExpertSystemTable> ExpertSystemTables = new List<ExpertSystemTable>();

        public string CalculateOutputValue(List<ExpertExpression> inputExpressions, IExpertVariable outputVar)
        {
            var sumOutputValues = new Double[outputVar.Values.Count];

            foreach(var table in ExpertSystemTables)
            {
                if(table.OutputVar.Name == outputVar.Name)
                {
                    var inputExpression = inputExpressions.FirstOrDefault(ie => ie.Variable.Name == table.InputVar.Name);
                    if (inputExpression != null)
                    {
                        for (int i = 0; i < outputVar.Values.Count; i++)
                        {
                            sumOutputValues[i] += table.ValuesTable[inputExpression.Variable.Values.IndexOf(inputExpression.Value)][i];
                        }
                    }
                }
            }

            return outputVar.Values[sumOutputValues.ToList().IndexOf(sumOutputValues.Max())];
        }
    }
}
