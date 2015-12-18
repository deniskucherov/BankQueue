using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common
{
    public sealed class Workplace
    {
        private readonly object _syncRoot = new object();
        private readonly LinkedList<Officer> _officers;
        
        public Workplace(QueueType queueType)
        {
            QueueType = queueType;
            _officers = new LinkedList<Officer>();
        }

        public QueueType QueueType { get; private set; }

        public void AddOfficer(Officer officer)
        {
            try
            {
                if (officer == null)
                    throw new ArgumentNullException(nameof(officer));

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
    }
}
