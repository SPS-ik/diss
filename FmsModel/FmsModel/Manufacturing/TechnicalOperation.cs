namespace FmsModel.Manufacturing
{
    public class TechnicalOperation
    {
        public int Id { get; set; }
        public TechnicalOperationTypes Type { get; set; }
        public int Duration { get; set; }

        public TechnicalOperation(int id,TechnicalOperationTypes type, int duration)
        {
            Id = id;
            Type = type;
            Duration = duration;
        }
    }
}