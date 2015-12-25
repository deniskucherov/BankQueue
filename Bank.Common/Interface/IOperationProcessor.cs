using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Value;

namespace Bank.Common.Interface
{
    public interface IOperationProcessor
    {
        IWorkProcess StartWorkplaceProccess(IWorkPlace workplace);
        void StopWorkplaceProccess(IWorkPlace workplace);
        void PauseWorkplaceProccess(IWorkPlace workplace);
        void DeleteWorkplaceProccess(IWorkPlace workplace);
        
        event EventHandler<CustomerArgs> ProcessCompleted;
    }
}
