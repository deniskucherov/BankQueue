using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;
using BankQueue.DomainEvents;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class QueueViewModel : CommonViewModel
    {
        private readonly System.Timers.Timer _timer;
        private readonly IQueueProcessor _queueProcessor;
        private readonly IQueueInformation _queueInformation;
        private readonly RoomMonitorSyncEvent _syncEvent;
       

        public QueueViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException(nameof(eventAggregator));
            
            // poor man's DI
            _queueProcessor = ServiceLocator.Current.GetInstance<IQueueProcessor>();
            _queueInformation = (IQueueInformation) _queueProcessor;
            _syncEvent = eventAggregator.GetEvent<RoomMonitorSyncEvent>();

            _timer = new Timer()
            {
                Interval = 500,
            };
            _timer.Elapsed += (sender, args) =>
            {
                OnPropertyChanged("");
                _syncEvent.Publish(null);
            };
            _timer.Start();

            eventAggregator.GetEvent<CustomerArrivedEvent>().Subscribe(OnCustomerArrived);
            eventAggregator.GetEvent<ClearQueueEvent>().Subscribe(OnClearQueueEvent);
        }

        public int TotalCustomers { get { return _queueInformation.TotalCustomersCount; } }
        public int CurrentCustomers { get {return _queueInformation.CurrentCustomersCount; } }
        public int Served { get { return TotalCustomers - CurrentCustomers; } }   

        private void OnCustomerArrived(CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            _queueProcessor.AddCustomer(args);
        }

        private void OnClearQueueEvent(QueueType queueType)
        {
            _queueProcessor.ClearQueue(queueType);
        }
    }
}
