using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;

namespace BankQueue.Core
{
    public sealed class OperationRoomProcessor
    {
        private readonly object _syncRoot = new object();
        private readonly List<WorkProcess> _processes;

        public OperationRoomProcessor()
        {
            _processes = new List<WorkProcess>();
        }

        public WorkProcess CreateNewProcess(Workplace workplace)
        {
            throw new NotImplementedException();   
        }


    }
}
