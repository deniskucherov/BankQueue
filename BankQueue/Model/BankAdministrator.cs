using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Interface;

namespace BankQueue.Model
{
    internal sealed class BankAdministrator : IAdministrator
    {
        public bool OfficeIsOpened { get; private set; }

        public BankAdministrator()
        {
            
        }

        public void OpenOffice()
        {
            OfficeIsOpened = true;
        }

        public void CloseOffice()
        {
            OfficeIsOpened = false;
        }
    }
}
