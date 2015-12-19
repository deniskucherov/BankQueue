using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service.Common;

namespace BankService
{
    public class ReportServiceManager : IReportService
    {
        public CustomerDto CustomersForPeriod(DateTime? dateTimeFrom, DateTime? dateTimeTo)
        {
            throw new NotImplementedException();
        }
    }
}
