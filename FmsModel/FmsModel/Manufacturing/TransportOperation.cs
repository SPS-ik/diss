namespace FmsModel.Manufacturing
{
    public class TransportOperation
    {
        public int Id { get; set; }
        private ILocation StartPosition { get; set; }
        private ILocation FinalPosition { get; set; }
        public int Duration { get; set; }

        public TransportOperation(int id, ILocation startPosition,
            ILocation finalPosition, int duration)
        {
            Id = id;
            StartPosition = startPosition;
            FinalPosition = finalPosition;
            Duration = duration;
        }
    }
}