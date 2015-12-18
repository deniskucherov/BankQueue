using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Interface;

namespace BankQueue.Core
{
    public sealed class WorkProcess
    {
        private readonly object _syncRoot = new object();
        private readonly Guid _id;
        private readonly Workplace _workplace;
        private readonly Timer _timer;

        private readonly IOperationQueue _operationQueue;

        public WorkProcess(Workplace workplace, IOperationQueue operationQueue)
        {
            if (workplace == null) throw new ArgumentNullException(nameof(workplace));
            if (operationQueue == null) throw new ArgumentNullException(nameof(operationQueue));

            _workplace = workplace;
            _operationQueue = operationQueue;
            State = WorkState.Stoped;
            _timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        public Guid ProcessId { get { return _id; } }
        public WorkState State { get; private set; }     

        public void Start()
        {
            State = WorkState.InWork;
            _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public void Stop()
        {
            State = WorkState.Stoped;
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void Pause()
        {
            State = WorkState.Paused;
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            try
            {
                if (!Monitor.TryEnter(_syncRoot)) return;
                WorkWithCustomer();
            }
            catch (Exception ex)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                throw new ApplicationException(string.Format("Oops! Critical WorkProcess error! ProcessId: {0}", _id), ex);
            }
            finally
            {
                Monitor.Exit(_timer);
            }
        }

        private void WorkWithCustomer()
        {
            var customerArgs = _operationQueue.GetNextCustomer(_workplace.QueueType);
            var officer = _workplace.GetNextOfficer();

            Thread.Sleep(500);
        }


    }
}
