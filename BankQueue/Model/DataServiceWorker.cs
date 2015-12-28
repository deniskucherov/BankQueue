using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service.Common;

namespace BankQueue.Model
{
    internal sealed class DataServiceWorker : ServiceWorker
    {
        private readonly ChannelFactory<IDataService> _dataServiceChannelFactory;
        private readonly ChannelFactory<IPingService> _pingServiceChannelFactory; 

        public DataServiceWorker()
            : base("net.tcp://localhost:8010/BankDataService")
        {
            var binding = new NetTcpBinding { TransactionFlow = true };
            var endpoint = new EndpointAddress(AddressUri);

            _dataServiceChannelFactory = new ChannelFactory<IDataService>(binding, endpoint);
            _pingServiceChannelFactory = new ChannelFactory<IPingService>(binding, endpoint);
        }

        public override void DoWork()
        {
            try
            {
                var service = _dataServiceChannelFactory.CreateChannel();
                var data = service.ServiceInformation();
                ((ICommunicationObject) service).Close();
            }
            catch (Exception ex)
            {
                HandleErrors(ex);
            }
        }

        protected override void Ping()
        {
            try
            {
                var service = _pingServiceChannelFactory.CreateChannel();
                service.Ping();
                ((ICommunicationObject) service).Close();
                ChangeStatus(ServiceConnectionStatus.Up);
            }
            catch
            {
                ChangeStatus(ServiceConnectionStatus.Down);
            }
        }
    }
}
