using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Interface;
using BankQueue.DomainEvents;
using Prism.Commands;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class EntranceViewModel : CommonServiceViewModel
    {
        private readonly IEntranceDemon _entranceDemon;
        private readonly ICustomerGenerator _customerGenerator;

        public EntranceViewModel(IEntranceDemon entranceDemon, IEventAggregator eventAggregator)
        {
            if (entranceDemon == null) throw new ArgumentNullException("entranceDemon");

            _entranceDemon = entranceDemon;

            _customerGenerator = entranceDemon as ICustomerGenerator;
            if (_customerGenerator != null)
            {
                _customerGenerator.CustomerArrivedEvent += (sender, args) => 
                { eventAggregator.GetEvent<CustomerArrivedEvent>().Publish(args); };
                
                _customerGenerator.GeneratorStopedEvent += (sender, args) =>
                {
                    base.ExecuteStopCommad();
                };
            }
        }


        protected override void ExecuteStartCommand()
        {
            _customerGenerator.Start();
            base.ExecuteStartCommand();
        }

        protected override void ExecuteStopCommad()
        {
            _customerGenerator.Stop();
            base.ExecuteStopCommad();
        }
    }
}
