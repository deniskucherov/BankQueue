using System;
using System.Threading;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;

namespace BankQueue.Core
{
    public sealed class EntranceDemon : IEntranceDemon, ICustomerSource
    {
        private readonly Timer _timer;

        public EntranceDemon()
        {
            _timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public IEntranceInformation Information { get; }
        public event EventHandler<CustomerArgs> CustomerArrivedEvent = delegate { };

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        private void Callback(object state)
        {
            var customer = new Customer("Customer " + DateTime.Now.Millisecond.ToString(), 25, Gender.M);

            var args = new CustomerArgs(customer, QueueType.Credit, new Payload());
            CustomerArrivedEvent(this, args);
        }

        
    }
}
