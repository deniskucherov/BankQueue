using System;
using Bank.Common.Value;

namespace Bank.Common.Interface
{
    public interface ICustomerSource
    {
        event EventHandler<CustomerArgs> CustomerArrivedEvent;
    }
}
