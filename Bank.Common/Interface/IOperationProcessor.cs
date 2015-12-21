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
        event EventHandler<CustomerArgs> ProcessCompleted;
    }
}
