using System.Collections.Generic;
using FmsModel.Manufacturing;

namespace FmsModel.Dispatchering
{
    public class RuleBasedDispatchingSystem: IDispatcheringSystem
    {
        public List<Product> Products { get; set; }

        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}