using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankQueue.Core.Annotations;
using BankQueue.DomainEvents;
using BankQueue.Model;
using Prism.Events;

namespace BankQueue.ViewModel
{
    public sealed class ReportServiceViewModel : CommonSyncViewModel
    {

        public ReportServiceViewModel([NotNull] IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            eventAggregator.GetEvent<ReportServiceConnectionStatusChanged>().Subscribe(OnConnectionStatusChanged);
        }

        public ServiceWorker.ServiceConnectionStatus Status { get; private set; }

        private void OnConnectionStatusChanged(ServiceWorker.ServiceConnectionStatus status)
        {
            _syncContext.Send(param =>
            {
                Status = (ServiceWorker.ServiceConnectionStatus)param;
                OnPropertyChanged(()=>Status);

            }, status);
        }
    }
}
