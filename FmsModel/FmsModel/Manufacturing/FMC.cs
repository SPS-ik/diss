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

        public List<TechnicalOperation> TechnicalOperations = new List<TechnicalOperation>();

        public FMC(int id, TechnicalOperationTypes type, ResourceStatus status = ResourceStatus.Idle)
        {
            Id = id;
            Type = type;
            Status = status;
        }

        public void AddToQuee(TechnicalOperation technicalOperation)
        {
            TechnicalOperations.Add(technicalOperation);
        }

        public void Process()
        {
            Status = ResourceStatus.Busy;
            var operation = TechnicalOperations[TechnicalOperations.Count - 1];

            Thread.Sleep(operation.Duration*100);

            TechnicalOperations.Remove(operation);
            Status = ResourceStatus.Idle;
        }
    }
}