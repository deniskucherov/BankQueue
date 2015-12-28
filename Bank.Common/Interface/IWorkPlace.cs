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
        WorkState State { get; }
        IOfficer CurrentOfficer { get; }
        Customer CurrentCustomer { get; }

        void AddOfficer(IOfficer officer);
        IOfficer GetNextOfficer();
        void SetParrentWorkProcess(IWorkProcess process);
    }
}
