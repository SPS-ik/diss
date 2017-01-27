namespace FmsModel.Manufacturing
{
    public class AGV
    {
        public int Id { get; set; }

        public ResourceStatus Status { get; set; }

        public void Transport(TransportOperation operation)
        {

        }
    }
}