using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    [ServiceContract]
    public interface IReportService
    {
        [OperationContract]
        ServiceInformationDto ServiceInformation();

        [OperationContract]
        CustomerDto CustomersForPeriod(DateTime? dateTimeFrom, DateTime? dateTimeTo);
    }
}
