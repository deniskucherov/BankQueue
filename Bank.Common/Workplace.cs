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
    public sealed class WorkPlace : IWorkPlace, IEquatable<WorkPlace>, INotifyPropertyChanged
    {
        private readonly object _syncRoot = new object();
        private readonly LinkedList<IOfficer> _officers;
        private IOfficer _currentOfficer;
        
        public WorkPlace(string name, QueueType queueType)
        {
            Name = name;
            QueueType = queueType;
            _officers = new LinkedList<IOfficer>();
        }

        public string Name { get; private set; }
        public QueueType QueueType { get; private set; }

        public IOfficer CurrentOfficer { get { return _currentOfficer; } }
        public IWorkProcess WorkProcess { get; private set; }

        public WorkState State
        {
            get { return WorkProcess == null ? WorkState.NaN : WorkProcess.State; }
        }    

        public void AddOfficer(IOfficer officer)
        {
            try
            {
                if (officer == null)
                    throw new ArgumentNullException("officer");

                lock (_syncRoot)
                {
                    _officers.AddLast(new LinkedListNode<IOfficer>(officer));
                    _currentOfficer = officer;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("AddOfficer error.", ex);
            }
        }

        public IOfficer GetNextOfficer()
        {
            if (_officers.Count == 0)
                return null;

            _currentOfficer = _officers.First.Value;
            OnPropertyChanged("CurrentOfficer");
            return _currentOfficer;
        }

        public void SetParrentWorkProcess(IWorkProcess process)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (WorkProcess != null) throw new ApplicationException("WorkProcess != null");
            
            WorkProcess = process;
            process.StateChanged += (sender, state) => OnPropertyChanged("State");
        }


        public bool Equals(WorkPlace other)
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
