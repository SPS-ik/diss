namespace FmsModel.Manufacturing
{
    public class FMС: ILocation
    {
        public int Id { get; set; }

        public TechnicalOperationTypes Type { get; set; }

        public ResourceStatus Status { get; set; }

        public void Process(TechnicalOperation operation)
        {

        }
    }
}