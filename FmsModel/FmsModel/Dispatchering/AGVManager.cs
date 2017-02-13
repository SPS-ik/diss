using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using FmsModel.Manufacturing;
using System;

namespace FmsModel.Dispatchering
{
    public class AGVManager
    {
        private List<AGV>  _agvList;
        private FMS _fms;

        public AGVManager(FMS fms)
        {
            _fms = fms;
            _agvList = _fms.Vehicles;
        }

        private AGV SelectAgv()
        {
            return _agvList.FirstOrDefault(a => a.CurrentTransportOperation == null)??
                _agvList.OrderBy(a => _fms.GetDuration(a.CurrentLocation, a.CurrentTransportOperation.Product.CurrentLocation) +
                _fms.GetDuration(a.CurrentTransportOperation.Product.CurrentLocation, a.CurrentTransportOperation.FinalPosition)).
                First();
        }

        public AGV AddToQuee(TransportOperation transportOperation)
        {
            var agv = SelectAgv();
            agv.AddToQuee(transportOperation);
            return agv;
        }

        public TransportOperation GetFromQuee(AGV agv)
        {
            TransportOperation transportOperation = null;
            if (agv.TransportOperations.Count > 0)
            {
                transportOperation = agv.TransportOperations[0];
                agv.TransportOperations.Remove(transportOperation);
            }
            agv.CurrentTransportOperation = transportOperation;
            return transportOperation;
        }

    }
}