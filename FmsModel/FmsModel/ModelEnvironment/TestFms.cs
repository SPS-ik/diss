using FmsModel.Dispatchering;
using FmsModel.Manufacturing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmsModel.ModelEnvironment
{
    public class TestFms
    {
        private FMS _fms = new FMS();

        public void InitFMS()
        {
            var cells = new List<FMC>
            {
                new FMC(1, TechnicalOperationTypes.Type1),
                new FMC(2, TechnicalOperationTypes.Type2),
                new FMC(3, TechnicalOperationTypes.Type3),
                new FMC(4, TechnicalOperationTypes.Type4)
            };

            var vehicles = new List<AGV>
            {
                new AGV(1),
                new AGV(2)
            };

            var warehouse = new Warehouse();
            
            _fms.Setup(cells, vehicles, warehouse);
        }

        public void Start()
        {
            var product1 = new Product(1,
                new List<TechnicalOperation>
                {
                    new TechnicalOperation(1, TechnicalOperationTypes.Type1, 10),
                    new TechnicalOperation(2, TechnicalOperationTypes.Type2, 9),
                    new TechnicalOperation(3, TechnicalOperationTypes.Type3, 8),
                    new TechnicalOperation(4, TechnicalOperationTypes.Type4, 7),
                });

            var product2 = new Product(2,
                new List<TechnicalOperation>
                {
                    new TechnicalOperation(1, TechnicalOperationTypes.Type2, 4),
                    new TechnicalOperation(2, TechnicalOperationTypes.Type3, 5),
                    new TechnicalOperation(3, TechnicalOperationTypes.Type4, 6),
                    new TechnicalOperation(4, TechnicalOperationTypes.Type1, 7),
                });

            var product3 = new Product(3,
                new List<TechnicalOperation>
                {
                    new TechnicalOperation(1, TechnicalOperationTypes.Type3, 5),
                    new TechnicalOperation(2, TechnicalOperationTypes.Type4, 6),
                    new TechnicalOperation(3, TechnicalOperationTypes.Type1, 7),
                    new TechnicalOperation(4, TechnicalOperationTypes.Type2, 8),
                });

            var product4 = new Product(4,
                new List<TechnicalOperation>
                {
                    new TechnicalOperation(1, TechnicalOperationTypes.Type4, 6),
                    new TechnicalOperation(2, TechnicalOperationTypes.Type1, 7),
                    new TechnicalOperation(3, TechnicalOperationTypes.Type2, 8),
                    new TechnicalOperation(4, TechnicalOperationTypes.Type3, 9),
                });
            
            var plan = new List<Product>()
            {
                product1,
                product2,
                product3,
                product4
            };

            InitFMS();

            var dispatcher = new RuleBasedDispatchingSystem();
            dispatcher.Start(_fms, plan);
        }
    }
}
