using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common
{
    public sealed class Department
    {
        public Department(string name, QueueType queueType)
        {
            Name = name;
            QueueType = queueType;
        }

        public string Name { get; private set; }
        public QueueType QueueType { get; private set; } 
    }
}
