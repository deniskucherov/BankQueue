using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Interface
{
    public interface IDepartmentManager
    {
        IWorkPlace CreateWorkplace(QueueType queueType);
        void DeleteWorkplace(IWorkPlace workPlace);
        IOfficer CreateOfficer();

        int WorkpPlacesCount { get; }
        int OfficersCount { get; }
    }
}
