using FmsModel.Dispatchering;
using FmsModel.Manufacturing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmsModel.ModelEnvironment
{
    static class TestFms
    {
        private static FMS _fms;
        public static void InitFMS()
        {
            var cells = new List<FMС>
            {
                new FMС(1, TechnicalOperationTypes.Type1),
                new FMС(2, TechnicalOperationTypes.Type2),
                new FMС(3, TechnicalOperationTypes.Type3),
                new FMС(4, TechnicalOperationTypes.Type4)
            };

            var vehicles = new List<AGV>
            {
                new AGV(1),
                new AGV(2)
            };

            var warehouse = new Warehouse();

            var dispatcher = new RuleBasedDispatchingSystem();

            _fms = new FMS(cells, vehicles, warehouse, dispatcher);
        }

        public static void Start()
        {
            var plan = new List<Product>()
            {
                new Product(1,
                    new List<TechnicalOperation>
                    {
                        new TechnicalOperation(1, TechnicalOperationTypes.Type1, 10),
                        new TechnicalOperation(2, TechnicalOperationTypes.Type1, 9),
                        new TechnicalOperation(3, TechnicalOperationTypes.Type1, 8),
                        new TechnicalOperation(4, TechnicalOperationTypes.Type1, 7),
                    })
            };
            _fms.DispatcheringSystem.Start(plan);
        }
    }
}
