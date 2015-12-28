using System;
using System.Collections.Generic;
using System.Threading;
using BankQueue.Core.Annotations;
using BankQueue.DomainEvents;
using Prism.Events;

namespace BankQueue.Model
{
    internal sealed class ServiceDemon
    {
        private readonly object _syncRoot = new object();
        private readonly Queue<ServiceWorker> _workersRoundRobinQueue;
        private readonly Timer _workTimer;

        private readonly IEventAggregator _eventAggregator;

        public ServiceDemon([NotNull] IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            _eventAggregator = eventAggregator;
            var worker1 = new DataServiceWorker();
            var worker2 = new ReportServiceWorker();

            worker1.StatusChanged += WorkerOnStatusChanged;
            worker2.StatusChanged += WorkerOnStatusChanged;

            _workersRoundRobinQueue = new Queue<ServiceWorker>(2);
            _workersRoundRobinQueue.Enqueue(worker1);
            _workersRoundRobinQueue.Enqueue(worker2);

            _workTimer = new Timer(WorkTimerCallback, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(1000));
        }

        private void WorkTimerCallback(object state)
        {
            if (!Monitor.TryEnter(_syncRoot)) return;

            try
            {
                Monitor.Enter(_syncRoot);
                var work = _workersRoundRobinQueue.Dequeue();
                work.DoWork();
                _workersRoundRobinQueue.Enqueue(work);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ServiceDemon timer error.", ex);
            }
            finally
            {
                Monitor.Exit(_syncRoot);
            }
        }

        private void WorkerOnStatusChanged(object sender, ServiceWorker.ServiceConnectionStatus status)
        {
            var worker = sender as ServiceWorker;
            if (worker == null)
                throw new ApplicationException("worker == null");

            if (worker is DataServiceWorker)
            {
                _eventAggregator.GetEvent<DataServiceConnectioinStatusChanged>().Publish(status);
            }else if (worker is ReportServiceWorker)
            {
                _eventAggregator.GetEvent<ReportServiceConnectionStatusChanged>().Publish(status);
            }
        }
    }
}
