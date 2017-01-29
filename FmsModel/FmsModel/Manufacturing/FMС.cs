namespace FmsModel.Manufacturing
{
    public class FMС: ILocation
    {
        public int Id { get; set; }

        public TechnicalOperationTypes Type { get; set; }

        public ResourceStatus Status { get; set; }

        public FMС(int id, TechnicalOperationTypes type, ResourceStatus status = ResourceStatus.Idle)
        {
            Id = id;
            Type = type;
            Status = status;
        }

        public void Process(TechnicalOperation operation)
        {

        }
    }
}