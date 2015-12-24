using System;
using System.Security.Cryptography.X509Certificates;
using Bank.Common.Value;

namespace Bank.Common.Interface
{
    public interface ICustomerGenerator
    {
        event EventHandler<CustomerArgs> CustomerArrivedEvent;
        event EventHandler<CustomerArgs> CustomerRejectedEvent;
        event EventHandler GeneratorStopedEvent;
 
        void AdjustCustomersGenerator(GeneratorArgs args);
        void Start();
        void Stop();
    }
}
