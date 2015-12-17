using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Interface;
using Bank.Common.Value;
using BankQueue.DomainEvents;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class QueueViewModel
    {
        private readonly IQueueProcessor _queueProcessor;

        public QueueViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            
            // poor man's DI
            _queueProcessor = ServiceLocator.Current.GetInstance<IQueueProcessor>();

            eventAggregator.GetEvent<CustomerArrivedEvent>().Subscribe(OnCustomerArrived);
        }

        private void OnCustomerArrived(CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            _queueProcessor.AddCustomer(args);
        }
    }
}
