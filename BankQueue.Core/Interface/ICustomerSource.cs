using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankQueue.Core.Value;

namespace BankQueue.Core.Interface
{
    public interface ICustomerSource
    {
        event EventHandler<CustomerArgs> CustomerArrivedEvent;
    }
}
