using System.Collections.Generic;
using System.Threading;
using System.Windows.Documents;

namespace FmsModel.Manufacturing
{
    public class FMC: ILocation
    {
        public int Id { get; set; }

        public TechnicalOperationTypes Type { get; set; }

        public ResourceStatus Status { get; set; }

        public Product CurrentProduct { get; set; }

        public List<Product> Products = new List<Product>();

        public FMC(int id, TechnicalOperationTypes type, ResourceStatus status = ResourceStatus.Idle)
        {
            Id = id;
            Type = type;
            Status = status;
        }

        public void AddToQuee(Product product)
        {
            Products.Add(product);
        }

        public void Process(Product product)
        {
            CurrentProduct = product;
        }
    }
}