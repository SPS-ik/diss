using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using FmsModel.Manufacturing;

namespace FmsModel.Dispatchering
{
    public class AGVManager
    {
        private List<AGV>  _agvList;

        public AGVManager(List<AGV> agvList)
        {
            _agvList = agvList;
        }

        public AGV GetIdleAgv()
        {
            return _agvList.FirstOrDefault(a => a.Status == ResourceStatus.Idle);
        }

    }
}