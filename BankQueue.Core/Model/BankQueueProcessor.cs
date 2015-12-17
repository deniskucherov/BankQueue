using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;

namespace BankQueue.Core.Model
{
    public sealed class BankQueueProcessor : IQueueProcessor
    {
        private readonly Dictionary<QueueType, BankQueueCommon> _workingQueues;
        private int _totalCustomersCount;
        private int _currentCustomersCount;

        public BankQueueProcessor(QueueFactory factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            _workingQueues = new Dictionary<QueueType, BankQueueCommon>(3);
            _workingQueues.Add(QueueType.Cashire, factory.CreateQueue(QueueType.Cashire));
            _workingQueues.Add(QueueType.Credit, factory.CreateQueue(QueueType.Credit));
            _workingQueues.Add(QueueType.Operational, factory.CreateQueue(QueueType.Operational));
        }

        public int QueueCount
        {
            get { return _workingQueues.Count; }
        }

        public int TotalCustomersCount { get { return _totalCustomersCount; } }
        public int CurrentCustomersCount { get { return _currentCustomersCount; } }

        public void AddCustomer(CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            var queue = _workingQueues[args.QueueType];
            if (queue == null)
                throw new ApplicationException("queue == null");

            queue.AddCustomer(args);
            Interlocked.Decrement(ref _totalCustomersCount);
        }

        public void CloseQueue(IEnumerable<QueueType> types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var t in types)
            {
                var queue = _workingQueues[t];
                if (queue == null)
                    throw new ApplicationException("queue == null");
                queue.Close();
            }
        }

        public CustomerArgs GetNextCustomer(QueueType type)
        {
            var queue = QueueByType(type);
            return queue.GetCustomer();
        }

        private BankQueueCommon QueueByType(QueueType type)
        {
            var queue = _workingQueues[type];
            if (queue == null)
                throw new ApplicationException("queue == null");

            return queue;
        }
    }
}
