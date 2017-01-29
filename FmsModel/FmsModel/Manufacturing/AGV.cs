namespace FmsModel.Manufacturing
{
    public class AGV
    {
        public int Id { get; set; }

        public ResourceStatus Status { get; set; }

        public AGV(int id, ResourceStatus status = ResourceStatus.Idle)
        {
            Id = id;
            Status = status;
        }

        public void Transport(TransportOperation operation)
        {

        }
    }
}