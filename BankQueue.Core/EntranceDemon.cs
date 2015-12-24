using System;
using System.Collections.Generic;
using System.Threading;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;

namespace BankQueue.Core
{
    public sealed class EntranceDemon : IEntranceDemon, ICustomerSource
    {
        private readonly Timer _timer;
        private readonly Random _rand = new Random(1);

        public EntranceDemon()
        {
            _timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
        }

        public IEntranceInformation Information { get; private set; }
        public event EventHandler<CustomerArgs> CustomerArrivedEvent = delegate { };

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        private void Callback(object state)
        {
            var gender = _rand.Next()%2 == 0 ? Gender.M : Gender.F;
            var customer = new Customer("Customer " + DateTime.Now.Millisecond.ToString(), 25, gender);

            var departments = new List<QueueType> {QueueType.Cashire, QueueType.Credit, QueueType.Operational};
            var type = departments[_rand.Next(0, 3)];
            var args = new CustomerArgs(customer, type, new Payload());

            CustomerArrivedEvent(this, args);
        }

        
    }
}
