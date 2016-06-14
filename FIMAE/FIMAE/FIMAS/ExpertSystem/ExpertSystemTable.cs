using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIMAE.FIMAS.ExpertSystem
{
    [Serializable]
    public class ExpertSystemTable
    {
        public IExpertVariable InputVar;
        public IExpertVariable OutputVar;
        public int[,] ValuesTable;

        public ExpertSystemTable(IExpertVariable inputVar, IExpertVariable outputVar)
        {
            InputVar = inputVar;
            OutputVar = outputVar;

            ValuesTable = new int[InputVar.Values.Count, OutputVar.Values.Count];
        }

        public bool TrySetValue(string inputValue, string outputValue, int expertValue)
        {
            if (!InputVar.Values.Contains(inputValue) || !OutputVar.Values.Contains(outputValue))
            {
                return false;
            }

            ValuesTable[InputVar.Values.IndexOf(inputValue), OutputVar.Values.IndexOf(outputValue)] = expertValue;
            return true;
        }
    }
}
