using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Value;
using Prism.Events;

namespace BankQueue.DomainEvents
{
    public sealed class CustomerServeStartsEvent : PubSubEvent<CustomerArgs>
    {
    }
}
