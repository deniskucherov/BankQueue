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
    public sealed class DataServiceViewModel : CommonSyncViewModel
    {

        public DataServiceViewModel([NotNull] IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            Status = ServiceWorker.ServiceConnectionStatus.NaN;
            eventAggregator.GetEvent<DataServiceConnectioinStatusChanged>().Subscribe(status =>
            {
                _syncContext.Post(state =>
                {
                    Status = status;
                    OnPropertyChanged(()=>Status);
                }, null);
            });
        }


        public ServiceWorker.ServiceConnectionStatus Status { get; private set; }
    }
}
