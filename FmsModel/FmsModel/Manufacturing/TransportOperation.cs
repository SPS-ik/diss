namespace FmsModel.Manufacturing
{
    public class TransportOperation
    {
        public int Id { get; set; }
        private ILocation StartPosition { get; set; }
        private ILocation FinalPosition { get; set; }
        public int Duration { get; set; }
    }
}