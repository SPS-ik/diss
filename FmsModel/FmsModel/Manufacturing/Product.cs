using System.Collections.Generic;

namespace FmsModel.Manufacturing
{
    public class Product
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public ProductStatus Status { get; set; }
        public List<TechnicalOperation> TechnicalOperations { get; set; }
    }
}