using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BankQueue.Core.Interface;
using BankQueue.Core.Value;

namespace BankQueue.Core.Model
{
    class BankQueueProcessor
    {
        private readonly Dictionary<QueueType, BankQueueCommon> _workingQueues;
        private int _totalCount;
        private int _currentCount;

        public BankQueueProcessor(QueueFactory factory, ICustomerSource customerSource)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (customerSource == null) throw new ArgumentNullException(nameof(customerSource));

            _workingQueues = new Dictionary<QueueType, BankQueueCommon>(3);
            _workingQueues.Add(QueueType.Cashire, factory.CreateQueue(QueueType.Cashire));
            _workingQueues.Add(QueueType.Credit, factory.CreateQueue(QueueType.Credit));
            _workingQueues.Add(QueueType.Operational, factory.CreateQueue(QueueType.Operational));

            customerSource.CustomerArrivedEvent += CustomerSourceOnCustomerArrivedEvent;
        }

        private void CustomerSourceOnCustomerArrivedEvent(object sender, CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            AddCustomer(args);
        }

        public int QueueCount
        {
            get { return _workingQueues.Count; }
        }

        public int TotalCount { get { return _totalCount; } }
        public int CurrentCount { get { return _currentCount; } }

        public void AddCustomer(CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            var queue = _workingQueues[args.QueueType];
            if (queue == null)
                throw new ApplicationException("queue == null");

            queue.AddCustomer(args);
            Interlocked.Decrement(ref _totalCount);
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
