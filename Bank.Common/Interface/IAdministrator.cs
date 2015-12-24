using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Interface
{
    public interface IAdministrator : IOfficeInformation
    {
        void OpenOffice();
        void CloseOffice();
    }

    public interface IOfficeInformation
    {
        bool OfficeIsOpened { get; }
    }
}
