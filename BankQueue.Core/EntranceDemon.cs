using System;
using System.Collections.Generic;
using System.Threading;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;
using BankQueue.Core.Annotations;

namespace BankQueue.Core
{
    public sealed class EntranceDemon : IEntranceDemon, ICustomerGenerator
    {
        private readonly object _syncRoot = new object();
        private readonly Timer _timer;
        private readonly Random _rand = new Random(1);
        private readonly IOfficeInformation _officeInformation;
        private GeneratorArgs _generatorArgs;
        private int _customerId;
        private List<QueueType> _queueTypes = new List<QueueType> { QueueType.Cashire, QueueType.Credit, QueueType.Operational };

        public EntranceDemon([NotNull] IOfficeInformation officeInformation)
        {
            if (officeInformation == null) throw new ArgumentNullException("officeInformation");

            _officeInformation = officeInformation;
            _generatorArgs = new GeneratorArgs(500, 3);

            _timer = new Timer(Callback, null, Timeout.Infinite, Timeout.Infinite);
        }

        public IEntranceInformation Information { get; private set; }

        public event EventHandler<CustomerArgs> CustomerArrivedEvent = delegate { };
        public event EventHandler<CustomerArgs> CustomerRejectedEvent = delegate {};
        public event EventHandler GeneratorStopedEvent = delegate {};

        public void Start()
        {
            _timer.Change(TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(_generatorArgs.GenerationPeriod));
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            GeneratorStopedEvent(this, null);
        }

        public void AdjustCustomersGenerator([NotNull] GeneratorArgs args)
        {
            try
            {
                lock (_syncRoot)
                {
                    _timer.Change(TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(args.GenerationPeriod));
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("AdjustCustomersGenerator error.", ex);
            }
        }

        private void Callback(object state)
        {
            if (!Monitor.TryEnter(_syncRoot)) return;
            try
            {
                var gender = _rand.Next() % 2 == 0 ? Gender.M : Gender.F;
                var age = _rand.Next(16, 85);
                var customerAge = new Age(age, AgeGroup.Create(age));
                var customerName = string.Format("Customer_{0}", ++_customerId); 
                var customer = new Customer(customerName, customerAge, gender);
                var type = _queueTypes[_rand.Next(0, 3)];
                
                var args = new CustomerArgs(customer, type, new Payload());

                if (_officeInformation.OfficeIsOpened)
                {
                    CustomerArrivedEvent(this, args);
                }
                else
                {
                    CustomerRejectedEvent(this, args);
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("EntranceDemon Timer error.", ex);
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }
    }
}
