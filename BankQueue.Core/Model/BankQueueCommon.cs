using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Value;

namespace BankQueue.Core.Model
{
    public abstract class BankQueueCommon
    {
        private readonly object _synkRoot = new object();
        protected readonly Queue<CustomerArgs> _customersQueue;

        public BankQueueCommon()
        {
            _customersQueue = new Queue<CustomerArgs>();
        }

        public abstract QueueType Type { get; }

        public virtual void AddCustomer(CustomerArgs customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            lock (_synkRoot)
            {
                _customersQueue.Enqueue(customer);
            }    
        }

        public virtual CustomerArgs GetCustomer()
        {
            lock (_synkRoot)
            {
                return _customersQueue.Dequeue();
            }
        }

        public virtual void Close()
        {
            lock (_synkRoot)
            {
                _customersQueue.Clear();
            }
        }

        
    }
}
