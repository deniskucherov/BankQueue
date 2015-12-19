using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service.Common;

namespace BankService
{
    public sealed class DataServiceManager : IDataService
    {
        public void SaveCustomers(IEnumerable<CustomerDto> customers)
        {
            
        }
    }
}
