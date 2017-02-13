using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FmsModel.Manufacturing;

namespace FmsModel.Dispatchering
{
    public class DiscreteDispatchingSystem : IDispatcheringSystem
    {
        private Dictionary<int, List<Action>> DiscreteEventsList =
            new Dictionary<int, List<Action>>();
        private FMCManager _fmcManager;
        private AGVManager _agvManager;
        private int _counter = 0;

        public FMS FMS { get; set; }

        public List<Product> Products { get; set; }

        public DiscreteDispatchingSystem(FMS fms, List<Product> products)
        {
            Products = products;
            FMS = fms;
            _fmcManager = new FMCManager(FMS.Cells);
            _agvManager = new AGVManager(FMS);
        }

        public void Start()
        { 
            InitDispatcher();

            while(Products.Any(p => p.Status != ProductStatus.Ready))
            {
                CheckSchedule(_counter++);
            }
        }

        private void InitDispatcher()
        {
            List<Action> initActions = new List<Action>();

            foreach (var product in Products)
            {
                initActions.Add(() => OnProductIsReadyForProcessing(product));
            }

            foreach(var cell in FMS.Cells)
            {
                initActions.Add(() => OnFmcIsIdle(cell));
            }

            foreach (var agv in FMS.Vehicles)
            {
                initActions.Add(() => OnAgvIsIdle(agv));
            }

            DiscreteEventsList.Add(0, initActions);
        }

        private void CheckSchedule(int counter)
        {
            List<Action> actions;
            if (DiscreteEventsList.TryGetValue(counter, out actions))
            {
                foreach (var action in actions)
                {
                    action?.Invoke();
                }
            }
        }

        private void OnProductIsReadyForProcessing(Product product)
        {
            var fmc = _fmcManager.AddToQuee(product);
            SendMessage(String.Format(
                $"Product {product.Id} is ready for processing operation {product.CurrentOperationIndex}. It was added to quee of FMC {fmc.Id}"));

        }

        private void OnFmcIsIdle(FMC cell)
        {
            if (cell.CurrentProduct != null)
            {
                SendMessage(String.Format(
                    $"Processing of Product {cell.CurrentProduct.Id} was finished on FMC {cell.Id}"));
                if (cell.CurrentProduct.CurrentOperationIndex < cell.CurrentProduct.TechnicalOperations.Count - 1)
                {
                    cell.CurrentProduct.CurrentOperationIndex++;
                    OnProductIsReadyForProcessing(cell.CurrentProduct);
                }
                else
                {
                    cell.CurrentProduct.Status = ProductStatus.Ready;
                }
            }

            var currentProduct = _fmcManager.GetFromQuee(cell);

            if (currentProduct != null)
            {
                var agv = _agvManager.AddToQuee(
                    new TransportOperation(1, currentProduct, cell, 5));
            }
            else
            {
                var nextCheckTime = _counter + 1;

                if (DiscreteEventsList.ContainsKey(nextCheckTime))
                {
                    DiscreteEventsList[nextCheckTime].
                        Add(() => OnFmcIsIdle(cell));
                }
                else
                {
                    DiscreteEventsList.Add(nextCheckTime,
                        new List<Action> { () => OnFmcIsIdle(cell) });
                }
            }
        }

        private void OnFmcReceivedProduct(FMC cell)
        {
            int nextCheckTime = 0;

            nextCheckTime = _counter + cell.CurrentProduct.
                TechnicalOperations[cell.CurrentProduct.CurrentOperationIndex].Duration;
            SendMessage(String.Format(
                $"Processing of Product {cell.CurrentProduct.Id} was started on FMC {cell.Id}"));

            if (DiscreteEventsList.ContainsKey(nextCheckTime))
            {
                DiscreteEventsList[nextCheckTime].
                    Add(() => OnFmcIsIdle(cell));
            }
            else
            {
                DiscreteEventsList.Add(nextCheckTime,
                    new List<Action> { () => OnFmcIsIdle(cell) });
            }
        }


        private void OnAgvIsIdle(AGV agv)
        {
            if (agv.CurrentTransportOperation != null)
            {
                agv.CurrentLocation = agv.CurrentTransportOperation.FinalPosition;
                SendMessage(String.Format(
                    $"Transportation of Product {agv.CurrentTransportOperation.Product.Id} was finished by AGV {agv.Id}"));

                var cell = agv.CurrentLocation as FMC;
                if (cell != null)
                {
                    OnFmcReceivedProduct(cell);
                }
            }

            var currentTransportOperation = _agvManager.GetFromQuee(agv);

            int nextCheckTime;
            if (currentTransportOperation == null)
            {
                nextCheckTime = _counter + 1;
            }
            else
            {
                nextCheckTime = _counter +
                    FMS.GetDuration(agv.CurrentLocation, currentTransportOperation.Product.CurrentLocation) +
                    FMS.GetDuration(currentTransportOperation.Product.CurrentLocation, currentTransportOperation.FinalPosition);
                SendMessage(String.Format(
                    $"Transportation of Product {agv.CurrentTransportOperation.Product.Id} was started by AGV {agv.Id}"));
            }

            if (DiscreteEventsList.ContainsKey(nextCheckTime))
            {
                DiscreteEventsList[nextCheckTime].
                    Add(() => OnAgvIsIdle(agv));
            }
            else
            {
                DiscreteEventsList.Add(nextCheckTime,
                    new List<Action> { () => OnAgvIsIdle(agv) });
            }
        }

        private void SendMessage(string message)
        {
            Console.WriteLine(String.Format($"{_counter} seconds: {message}"));
        }
    }
}
