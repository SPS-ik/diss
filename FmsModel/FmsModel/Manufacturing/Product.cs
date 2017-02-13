using System.Collections.Generic;

namespace FmsModel.Manufacturing
{
    public class Product
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public ProductStatus Status { get; set; }
        public ILocation CurrentLocation { get; set; }
        public List<TechnicalOperation> TechnicalOperations { get; set; }
        public int CurrentOperationIndex { get; set; }

        public Product(int id, List<TechnicalOperation> operations, int priority = 1,
            ProductStatus status = ProductStatus.Blank)
        {
            Id = id;
            TechnicalOperations = operations;
            Priority = priority;
            CurrentOperationIndex = 0;
        }
    }
}