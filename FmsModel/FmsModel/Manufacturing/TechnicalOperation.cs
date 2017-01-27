namespace FmsModel.Manufacturing
{
    public class TechnicalOperation
    {
        public int Id { get; set; }
        public TechnicalOperationTypes Type { get; set; }
        public int Duration { get; set; }
    }
}