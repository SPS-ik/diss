namespace FmsModel.Manufacturing
{
    public class TransportOperation
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public ILocation FinalPosition { get; set; }
        public int Duration { get; set; }

        public TransportOperation(int id, Product product,
            ILocation finalPosition, int duration)
        {
            Id = id;
            Product = product;
            FinalPosition = finalPosition;
            Duration = duration;
        }
    }
}