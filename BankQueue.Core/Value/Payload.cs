using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueue.Core.Value
{
    public sealed class Payload
    {
        public OperationType Operation { get; private set; }
    }
}
