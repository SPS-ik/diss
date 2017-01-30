using System.Collections.Generic;
using System.Linq;
using FmsModel.Manufacturing;

namespace FmsModel.Dispatchering
{
    public class FMCManager
    {
        private List<FMC> _fmcList;

        public FMCManager(List<FMC> fmcList)
        {
            _fmcList = fmcList;
        }

        public FMC AddToQuee(TechnicalOperation technicalOperation)
        {
            var fmc = _fmcList.FirstOrDefault(f => f.Type == technicalOperation.Type && f.Status == ResourceStatus.Idle);
            fmc?.AddToQuee(technicalOperation);
            return fmc;
        }
    }
}