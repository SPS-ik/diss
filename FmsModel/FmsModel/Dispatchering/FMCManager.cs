using System.Collections.Generic;
using System.Linq;
using FmsModel.Manufacturing;

namespace FmsModel.Dispatchering
{
    public class FMCManager
    {
        private List<FMC> _fmcList;

        public FMCManager(List<FMC> fmcList)
        {
            _fmcList = fmcList;
        }

        public FMC AddToQuee(Product product)
        {
            var technicalOperation = product.TechnicalOperations[product.CurrentOperationIndex];
            var fmc = _fmcList.FirstOrDefault(f => f.Type == technicalOperation.Type);

            fmc?.AddToQuee(product);
            return fmc;
        }

        public Product GetFromQuee(FMC cell)
        {
            Product product = null;
            if (cell.Products.Count > 0)
            {
                product = cell.Products[0];
                cell.Products.Remove(product);
            }
            cell.CurrentProduct = product;
            return product;
        }
    }
}