using System.Collections.Generic;
using FmsModel.Dispatchering;

namespace FmsModel.Manufacturing
{
    public class FMS
    {
        public List<FMC> Cells { get; set; }
        public List<AGV> Vehicles { get; set; }
        public Warehouse Warehouse { get; set; }

        public FMS()
        {
        }

        public FMS(List<FMC> cells, List<AGV> vehicles, Warehouse warehouse)
        {
            Setup(cells, vehicles, warehouse);
        }

        public void Setup(List<FMC> cells, List<AGV> vehicles, Warehouse warehouse)
        {
            Cells = cells;
            Vehicles = vehicles;
            Warehouse = warehouse;
        }
    }
}