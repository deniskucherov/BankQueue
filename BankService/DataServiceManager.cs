using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service.Common;
using System.Transactions;

namespace BankService
{
    [ServiceBehavior(UseSynchronizationContext = false, 
        TransactionIsolationLevel = IsolationLevel.Serializable, TransactionTimeout = "00:00:30")]
    public sealed class DataServiceManager : IDataService, IPingService
    {
        public ServiceInformationDto ServiceInformation()
        {
            return new ServiceInformationDto
            {
                ServiceName = "DataService",
                Version = 1
            };
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SaveCustomers(IEnumerable<CustomerDto> customers)
        {
            
        }

        public void Ping()
        {
            
        }
    }
}
