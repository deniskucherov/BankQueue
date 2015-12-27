using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Value;

namespace Bank.Common.Interface
{
    public interface IWorkProcess
    {
        event EventHandler<CustomerArgs> ProcessStarted;
        event EventHandler<CustomerArgs> ProcessCompleted;
        event EventHandler<WorkState> StateChanged;

        Guid ProcessId { get; }
        WorkState State { get; }
        IWorkPlace Workplace { get; }

        void Start();
        void Stop();
        void Pause();
    }
}
