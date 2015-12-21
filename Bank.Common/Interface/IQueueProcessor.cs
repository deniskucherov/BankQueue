using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Value;

namespace Bank.Common.Interface
{
    public interface IOperationQueue
    {
        CustomerArgs GetNextCustomer(QueueType type);
    }

    public interface IQueueProcessor : IOperationQueue
    { 
        void AddCustomer(CustomerArgs args);
        void ClearQueue(IEnumerable<QueueType> types);
        void ClearQueue(QueueType type);
        void CloseAndClearQueue(IEnumerable<QueueType> types);
        void CloseQueue(IEnumerable<QueueType> types);
        void OpenQueue(IEnumerable<QueueType> types);
    }
}
