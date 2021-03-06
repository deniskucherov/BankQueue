﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.NotAllowed)]
        ServiceInformationDto ServiceInformation();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        void SaveCustomers(IEnumerable<CustomerDto> customers);
    }
}
