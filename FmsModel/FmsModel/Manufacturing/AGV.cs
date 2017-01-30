using System.Threading;

namespace FmsModel.Manufacturing
{
    public class AGV
    {
        public int Id { get; set; }

        public ResourceStatus Status { get; set; }

        public ILocation CurrentLocation { get; set; }

        public AGV(int id, ResourceStatus status = ResourceStatus.Idle)
        {
            Id = id;
            Status = status;
        }

        public void Transport(Product product, ILocation finalPosition, int duration)
        {
            Status = ResourceStatus.Busy;

            Thread.Sleep(duration); //to start
            Thread.Sleep(duration); //to final

            product.CurrentLocation = finalPosition;
            CurrentLocation = finalPosition;
            Status = ResourceStatus.Idle;
        }
    }
}