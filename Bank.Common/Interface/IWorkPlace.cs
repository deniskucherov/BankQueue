using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Interface
{
    public interface IWorkPlace
    {
        string Name { get; }
        QueueType QueueType { get; }
        void AddOfficer(Officer officer);
        Officer GetNextOfficer();
    }
}
