using System.Collections.Generic;
using FmsModel.Manufacturing;

namespace FmsModel.Dispatchering
{
    public interface IDispatcheringSystem
    {
        List<Product> Products { get; set; }
        void Start();
    }
}