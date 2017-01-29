﻿using System.Collections.Generic;
using FmsModel.Dispatchering;

namespace FmsModel.Manufacturing
{
    public class FMS
    {
        public List<FMС> Cells { get; set; }
        public List<AGV> Vehicles { get; set; }
        public Warehouse Warehouse { get; set; }
        public IDispatcheringSystem DispatcheringSystem { get; set; }

        public FMS(List<FMС> cells, List<AGV> vehicles,
            Warehouse warehouse, IDispatcheringSystem dispatcheringSystem)
        {
            Cells = cells;
            Vehicles = vehicles;
            Warehouse = warehouse;
            DispatcheringSystem = dispatcheringSystem;
        }
    }
}