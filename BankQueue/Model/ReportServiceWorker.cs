using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service.Common;

namespace BankQueue.Model
{
    public sealed class ReportServiceWorker : ServiceWorker
    {
        private readonly ChannelFactory<IPingService> _pingServiceChannelFactory; 

        public ReportServiceWorker()
            : base("net.tcp://localhost:8011/BankReportService")
        {
            var binding = new NetTcpBinding { TransactionFlow = false };
            var endpoint = new EndpointAddress(AddressUri);

            _pingServiceChannelFactory = new ChannelFactory<IPingService>(binding, endpoint);
        }

        public override void DoWork()
        {
            
        }

        protected override void Ping()
        {
            try
            {
                var service = _pingServiceChannelFactory.CreateChannel();
                service.Ping();
                ((ICommunicationObject)service).Close();
                ChangeStatus(ServiceConnectionStatus.Up);
            }
            catch
            {
                ChangeStatus(ServiceConnectionStatus.Down);
            }
        }
    }
}
