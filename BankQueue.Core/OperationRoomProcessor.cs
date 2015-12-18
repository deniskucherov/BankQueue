using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Interface;

namespace BankQueue.Core
{
    public sealed class OperationRoomProcessor : IOperationProcessor
    {
        private readonly object _syncRoot = new object();
        private readonly IOperationQueue _operationQueue;
        private readonly List<WorkProcess> _processes;

        public OperationRoomProcessor(IOperationQueue operationQueue)
        {
            if (operationQueue == null) throw new ArgumentNullException(nameof(operationQueue));

            _operationQueue = operationQueue;
            _processes = new List<WorkProcess>();
        }

        public IWorkProcess StartWorkplaceProccess(IWorkPlace workplace)
        {
            var process = new WorkProcess(workplace, _operationQueue);
            _processes.Add(process);
            process.Start();

            return process;
        }

        public void PauseWorkplaceProccess(IWorkPlace workplace)
        {
            
        }

        public void StopWorkplaceProccess(Workplace workplace)
        {
            
        }

        public void DeleteWorkplaceProccess(Workplace workplace)
        {
            
        }


    }
}
