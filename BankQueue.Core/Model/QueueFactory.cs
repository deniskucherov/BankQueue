using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;

namespace BankQueue.Core.Model
{
    public sealed class QueueFactory
    {
        public BankQueueCommon CreateQueue(QueueType type)
        {
            switch (type)
            {
                case QueueType.Cashire:
                    return new CashireBankQueue();
                case QueueType.Credit:
                    return new CreditBankQueue();
                case QueueType.Operational:
                    return new OperationalBankQueue();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
