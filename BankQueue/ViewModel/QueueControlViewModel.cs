using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Bank.Common;
using Bank.Common.Value;
using BankQueue.Core.Annotations;
using BankQueue.DomainEvents;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class QueueControlViewModel : CommonViewModel
    {
        private readonly object _syncRoot = new object();
        private readonly IEventAggregator _eventAggregator;

        private ICommand _clearQueueCommand;

        public QueueControlViewModel()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            Init();
        }

        public QueueControlViewModel([NotNull] IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            _eventAggregator = eventAggregator;
            Init();
        }

        private void Init()
        {
            Customers = new ObservableCollection<CustomerArgs>();
            _eventAggregator.GetEvent<RoomMonitorSyncEvent>().Subscribe(o => { OnPropertyChanged(""); });
            _eventAggregator.GetEvent<CustomerArrivedEvent>().Subscribe(OnCustomerArrived);
            _eventAggregator.GetEvent<CustomerServedEvent>().Subscribe(OnCustomerServed);
        }

        public QueueType QueueType { get; set; }
        public ObservableCollection<CustomerArgs>  Customers { get; set; }
        public int CustomersCount { get { return Customers.Count; } }

        public ICommand ClearQueueCommand
        {
            get { return _clearQueueCommand ?? (_clearQueueCommand = new DelegateCommand(CleaQueueCommand)); }
        }

        private void CleaQueueCommand()
        {
            if (MessageBox.Show("Remove all customers from current queue?", "Clear queue", MessageBoxButton.YesNoCancel) != MessageBoxResult.Yes)
            {
                return;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_syncRoot)
                {
                    Customers.Clear();
                    _eventAggregator.GetEvent<ClearQueueEvent>().Publish(QueueType);
                }
            });
        }

        private void OnCustomerArrived(CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException("args");

            if (args.QueueType != QueueType) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_syncRoot)
                {
                    Customers.Add(args);
                }
            });
        }

        private void OnCustomerServed([NotNull] CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException("args");

            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_syncRoot)
                {
                    var customer = Customers.SingleOrDefault(x => x.Id == args.Id);
                    if (customer == null) return;
                    Customers.Remove(customer);
                }
            });
        }
    }
}
