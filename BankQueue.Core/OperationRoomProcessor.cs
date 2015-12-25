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
        private readonly List<IWorkProcess> _processes;

        public OperationRoomProcessor(IOperationQueue operationQueue, IStampProvider stampProvider)
        {
            if (operationQueue == null) throw new ArgumentNullException("operationQueue");
            if (stampProvider == null) throw new ArgumentNullException("stampProvider");

            _operationQueue = operationQueue;
            _stampProvider = stampProvider;
            _processes = new List<IWorkProcess>();
        }

        public event EventHandler<CustomerArgs> ProcessCompleted = delegate { };

        public IWorkProcess StartWorkplaceProccess(IWorkPlace workplace)
        {
            try
            {
                if (workplace == null) throw new ArgumentNullException("workplace");
                IWorkProcess process = null;

                lock (_syncRoot)
                {
                    process = _processes.SingleOrDefault(x => x.Workplace.Equals(workplace));
                    if (process == null)
                    {
                        process = new WorkProcess(workplace, _operationQueue, _stampProvider);
                        process.ProcessCompleted += (sender, args) => { ProcessCompleted(sender, args); };
                        _processes.Add(process);
                    }

                    process.Start();
                }

                return process;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("StartWorkplaceProccess error.", ex);
            }
        }

        public void PauseWorkplaceProccess(IWorkPlace workplace)
        {
            
        }

        public void StopWorkplaceProccess(IWorkPlace workplace)
        {
            try
            {
                if (workplace == null) throw new ArgumentNullException("workplace");
                lock (_syncRoot)
                {
                    var process = _processes.SingleOrDefault(x => x.Workplace.Equals(workplace));
                    if (process == null)
                        throw new ApplicationException("process == null");
                    process.Stop();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("StopWorkplaceProccess error.", ex);
            }
        }

        public void DeleteWorkplaceProccess(IWorkPlace workplace)
        {
            try
            {
                if (workplace == null) throw new ArgumentNullException("workplace");
                lock (_syncRoot)
                {
                    var process = _processes.SingleOrDefault(x => x.Workplace.Equals(workplace));
                    if (process == null)
                        throw new ApplicationException("process == null");
                    process.Stop();
                    _processes.Remove(process);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("StopWorkplaceProccess error.", ex);
            }
        }

    }
}
