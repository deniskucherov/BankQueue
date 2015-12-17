using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Interface;
using BankQueue.DomainEvents;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class EntranceViewModel : CommonServiceViewModel
    {
        private readonly IEntranceDemon _entranceDemon;

        public EntranceViewModel(IEntranceDemon entranceDemon, IEventAggregator eventAggregator)
        {
            if (entranceDemon == null) throw new ArgumentNullException(nameof(entranceDemon));

            _entranceDemon = entranceDemon;

            var customerSource = entranceDemon as ICustomerSource;
            if (customerSource != null)
            {
                customerSource.CustomerArrivedEvent += (sender, args) => 
                { eventAggregator.GetEvent<CustomerArrivedEvent>().Publish(args); };
            }
        }

        protected override void ExecuteStartCommand()
        {
            _entranceDemon.Start();
        }

        protected override void ExecuteStopCommad()
        {
            _entranceDemon.Stop();
        }
    }
}
