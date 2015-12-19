using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Interface
{
    public interface IQueueInformation
    {
        int QueueCount { get; }
        int TotalCustomersCount { get; }
        int CurrentCustomersCount { get; }

        int QueueCustomersCount(QueueType type);
    }
}
