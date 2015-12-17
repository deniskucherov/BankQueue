using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueue.Core.Model
{
    public sealed class OperationalBankQueue : BankQueueCommon
    {
        public override QueueType Type { get {return QueueType.Operational;} }
    }
}
