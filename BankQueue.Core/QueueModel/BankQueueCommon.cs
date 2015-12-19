using System;
using System.Collections.Generic;
using Bank.Common;
using Bank.Common.Value;

namespace BankQueue.Core.QueueModel
{
    public abstract class BankQueueCommon
    {
        
        protected readonly Queue<CustomerArgs> _customersQueue;

        public BankQueueCommon()
        {
            _customersQueue = new Queue<CustomerArgs>();
            State = QueueState.Opened;
        }

        public abstract QueueType Type { get; }
        public QueueState State { get; private set; }

        public int Count { get { return _customersQueue.Count; } }

        public virtual void AddCustomer(CustomerArgs customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (State == QueueState.Closed)
                return;
            _customersQueue.Enqueue(customer);  
        }

        public virtual CustomerArgs GetCustomer()
        {
            return _customersQueue.Count > 0 ? _customersQueue.Dequeue() : null;
        }

        public virtual void Open()
        {
            State = QueueState.Opened;
        }

        public virtual void Close()
        {
            State = QueueState.Closed;
        }

        public virtual void CloseAndClear()
        {
            Close();
            _customersQueue.Clear();
        }

    }
}
