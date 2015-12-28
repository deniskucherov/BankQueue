using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankQueue.Model;
using Prism.Events;

namespace BankQueue.DomainEvents
{
    public sealed class ReportServiceConnectionStatusChanged : PubSubEvent<ServiceWorker.ServiceConnectionStatus>
    {
    }
}
