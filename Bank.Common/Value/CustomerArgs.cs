using System;

namespace Bank.Common.Value
{
    public sealed class CustomerArgs
    {
        public CustomerArgs(Customer customer, QueueType queueType, Payload operationPayload)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (operationPayload == null) throw new ArgumentNullException(nameof(operationPayload));

            Id = Guid.NewGuid();
            Customer = customer;
            QueueType = queueType;
            OperationPayload = operationPayload;
        }

        public Guid Id { get; private set; }
        public Customer Customer { get; private set; }
        public QueueType QueueType { get; private set; }
        public Payload OperationPayload { get; private set; }
    }
}
