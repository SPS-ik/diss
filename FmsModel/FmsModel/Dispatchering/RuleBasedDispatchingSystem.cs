﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FmsModel.Manufacturing;

namespace FmsModel.Dispatchering
{
    public class RuleBasedDispatchingSystem: IDispatcheringSystem
    {
        FMCManager _fmcManager;
        AGVManager _agvManager;

        public List<Product> Products { get; set; }
        public FMS FMS { get; set; }

        public RuleBasedDispatchingSystem(FMS fms, List<Product> products)
        {
            Products = products;
            FMS = fms;

            _fmcManager = new FMCManager(FMS.Cells);
            _agvManager = new AGVManager(FMS);
           
        }

        public void Start()
        {
            while (Products.Any(p => p.Status==ProductStatus.Blank))
            {
                var blankProduct = Products.Where(p => p.Status == ProductStatus.Blank).
                    OrderByDescending(i => i.Priority).FirstOrDefault();
                if (blankProduct != null)
                {
                    blankProduct.CurrentLocation = FMS.Warehouse;

                    blankProduct.Status = ProductStatus.InProgress;

                    Task.Factory.StartNew(() => ProcessProduct(blankProduct)).
                        ContinueWith((res) => { blankProduct.Status = ProductStatus.Ready; });

                }
            }
        }

        private void ProcessProduct(Product product)
        {
            foreach (var technicalOperation in product.TechnicalOperations)
            {
                //FMC fmc = _fmcManager.AddToQuee(technicalOperation);

                //TransportProduct(product, fmc);

                //fmc.Process();
            }
        }

        private void TransportProduct(Product product, ILocation location)
        {
            AGV agv = null;
            
            agv.Transport(product, location, 10);
        }
    }
}