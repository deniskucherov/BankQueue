using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Interface;

namespace BankQueue.Core
{
    public sealed class DepartmentManager : IDepartmentManager
    {
        private object _syncRoot = new object();

        private readonly List<IWorkPlace> _workplaces;
        private readonly List<IOfficer> _officers;
        private readonly Random _random;
 
        private int _workplaceId;
        private int _officerId;

        public DepartmentManager()
        {
            _workplaces = new List<IWorkPlace>();
            _officers = new List<IOfficer>();
            _random = new Random();
        }

        public int WorkpPlacesCount { get { return _workplaces.Count; } }
        public int OfficersCount { get { return _officers.Count; } }

        public void DeleteWorkplace(IWorkPlace workPlace)
        {
            try
            {
                if (workPlace == null) throw new ArgumentNullException("workPlace");
                
            }
            catch (Exception ex)
            {
                
            }
        }

        public IOfficer CreateOfficer()
        {
            IOfficer officer = null;
            var gender = DateTime.Now.Millisecond % 2 == 0 ? Gender.F : Gender.M;
            var age = _random.Next(24, 46);

            lock (_syncRoot)
            {
                var personName = string.Format("Officer_{0}", ++_officerId);
                var person = new Person(personName, age, gender);
                officer = new Officer(person);
                _officers.Add(officer);
            }

            return officer;
        }

        public IWorkPlace CreateWorkplace(QueueType queueType)
        {
            IWorkPlace workPlace = null;

            lock (_syncRoot)
            {
                var name = string.Format("WorkPlace #{0}", ++_workplaceId);
                workPlace = new WorkPlace(name, queueType);
                _workplaces.Add(workPlace);
            }

            return workPlace;
        }

    }
}
