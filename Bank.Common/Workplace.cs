using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Interface;

namespace Bank.Common
{
    public sealed class Workplace : IWorkPlace, IEquatable<Workplace>
    {
        private readonly object _syncRoot = new object();
        private readonly LinkedList<Officer> _officers;
        
        public Workplace(string name, QueueType queueType)
        {
            Name = name;
            QueueType = queueType;
            _officers = new LinkedList<Officer>();
        }

        public string Name { get; private set; }
        public QueueType QueueType { get; private set; }

        public void AddOfficer(Officer officer)
        {
            try
            {
                if (officer == null)
                    throw new ArgumentNullException("officer");

                lock (_syncRoot)
                {
                    _officers.AddLast(new LinkedListNode<Officer>(officer));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("AddOfficer error.", ex);
            }
        }

        public Officer GetNextOfficer()
        {
            if (_officers.Count == 0)
                return null;

            return _officers.First.Value;

        }

        public bool Equals(Workplace other)
        {
            return Name.Equals(other.Name);
        }
    }
}
