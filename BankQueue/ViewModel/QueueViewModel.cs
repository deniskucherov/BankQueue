using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;
using BankQueue.DomainEvents;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class QueueViewModel : CommonViewModel
    {
        private readonly System.Timers.Timer _timer;
        private readonly IQueueProcessor _queueProcessor;
        private readonly IQueueInformation _queueInformation;

        public QueueViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            
            // poor man's DI
            _queueProcessor = ServiceLocator.Current.GetInstance<IQueueProcessor>();
            _queueInformation = (IQueueInformation) _queueProcessor;

            _timer = new Timer()
            {
                Interval = 300,
            };
            _timer.Elapsed += (sender, args) => { OnPropertyChanged(""); };
            _timer.Start();

            eventAggregator.GetEvent<CustomerArrivedEvent>().Subscribe(OnCustomerArrived);
        }

        public int TotalCustomers { get { return _queueInformation.TotalCustomersCount; } }

        public int CredtiQueueCustomers { get { return _queueInformation.QueueCustomersCount(QueueType.Credit); } }
        public int CashireQueueCustomers { get { return _queueInformation.QueueCustomersCount(QueueType.Operational); } }
        public int OperationalQueueCustomers { get { return _queueInformation.QueueCustomersCount(QueueType.Cashire); } }

        private void OnCustomerArrived(CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            _queueProcessor.AddCustomer(args);
        }
    }
}
