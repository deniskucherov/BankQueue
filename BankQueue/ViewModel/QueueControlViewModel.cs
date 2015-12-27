using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public sealed class QueueControlViewModel : CommonSyncViewModel
    {
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
            _eventAggregator.GetEvent<CustomerServeStartsEvent>().Subscribe(OnCustomerServeStarts);
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

            // using app dispatcher as an example..
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

            // async call
            _syncContext.Post(state =>
            {
                lock (_syncRoot)
                {
                    Customers.Add(args);
                }
            }, null);
        }

        private void OnCustomerServed([NotNull] CustomerArgs args)
        {
            if (args == null) throw new ArgumentNullException("args");

        }

        private void OnCustomerServeStarts(CustomerArgs args)
        {
            try
            {
                if (args == null) throw new ArgumentNullException("args");

                // sync call
                _syncContext.Send(state =>
                {
                    lock (_syncRoot)
                    {
                        var customer = Customers.SingleOrDefault(x => x.Id == args.Id);
                        if (customer == null) return;
                        Customers.Remove(customer);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                HandleException("OnCustomerServeStarts error.", ex);
            }
        }
    }
}
