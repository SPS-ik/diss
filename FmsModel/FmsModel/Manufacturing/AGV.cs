using System.Collections.Generic;
using System.Threading;

namespace FmsModel.Manufacturing
{
    public class AGV
    {
        public int Id { get; set; }

        public ResourceStatus Status { get; set; }

        public TransportOperation CurrentTransportOperation { get; set; }

        public List<TransportOperation> TransportOperations = new List<TransportOperation>();

        public ILocation CurrentLocation { get; set; }

        public AGV(int id, ResourceStatus status = ResourceStatus.Idle)
        {
            Id = id;
            Status = status;
        }

        public void AddToQuee(TransportOperation transportOperation)
        {
            TransportOperations.Add(transportOperation);
        }

        public void Transport(Product product, ILocation finalPosition, int duration)
        {
            Status = ResourceStatus.Busy;
            product.CurrentLocation = finalPosition;
            CurrentLocation = finalPosition;
            Status = ResourceStatus.Idle;
        }
    }
}