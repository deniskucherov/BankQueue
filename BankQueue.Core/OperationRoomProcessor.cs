using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Interface;
using Bank.Common.Value;

namespace BankQueue.Core
{
    public sealed class OperationRoomProcessor : IOperationProcessor
    {
        private readonly object _syncRoot = new object();
        private readonly IOperationQueue _operationQueue;
        private readonly IStampProvider _stampProvider;
        private readonly List<WorkProcess> _processes;

        public OperationRoomProcessor(IOperationQueue operationQueue, IStampProvider stampProvider)
        {
            if (operationQueue == null) throw new ArgumentNullException("operationQueue");
            if (stampProvider == null) throw new ArgumentNullException("stampProvider");

            _operationQueue = operationQueue;
            _stampProvider = stampProvider;
            _processes = new List<WorkProcess>();
        }

        public event EventHandler<CustomerArgs> ProcessCompleted = delegate { };

        public IWorkProcess StartWorkplaceProccess(IWorkPlace workplace)
        {
            var process = new WorkProcess(workplace, _operationQueue, _stampProvider);
            process.ProcessCompleted += (sender, args) => { ProcessCompleted(sender, args); };
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
