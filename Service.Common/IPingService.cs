using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    [ServiceContract]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        [TransactionFlow(TransactionFlowOption.NotAllowed)]
        void Ping();
    }
}
