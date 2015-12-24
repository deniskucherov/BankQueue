using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Interface;

namespace Bank.Common
{
    public sealed class Workplace : IWorkPlace, IEquatable<Workplace>, INotifyPropertyChanged
    {
        private readonly object _syncRoot = new object();
        private readonly LinkedList<Officer> _officers;
        private Officer _currentOfficer;
        
        public Workplace(string name, QueueType queueType)
        {
            Name = name;
            QueueType = queueType;
            _officers = new LinkedList<Officer>();
        }

        public string Name { get; private set; }
        public QueueType QueueType { get; private set; }

        public Officer CurrentOfficer { get { return _currentOfficer; } }

        public void AddOfficer(Officer officer)
        {
            try
            {
                if (officer == null)
                    throw new ArgumentNullException("officer");

                lock (_syncRoot)
                {
                    _officers.AddLast(new LinkedListNode<Officer>(officer));
                    _currentOfficer = officer;
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

            _currentOfficer = _officers.First.Value;
            OnPropertyChanged("CurrentOfficer");
            return _currentOfficer;
        }

        public bool Equals(Workplace other)
        {
            return Name.Equals(other.Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
