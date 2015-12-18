using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Value;

namespace Bank.Common.Interface
{
    public interface IQueueProcessor
    {
        int QueueCount { get; }
        int TotalCustomersCount { get; }
        int CurrentCustomersCount { get; }

        void AddCustomer(CustomerArgs args);
        void CloseAndClearQueue(IEnumerable<QueueType> types);
        void CloseQueue(IEnumerable<QueueType> types);
        void OpenQueue(IEnumerable<QueueType> types);
        CustomerArgs GetNextCustomer(QueueType type);
    }
}
