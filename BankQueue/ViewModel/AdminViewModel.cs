using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bank.Common.Interface;
using BankQueue.Core.Annotations;
using Prism.Commands;

namespace BankQueue.ViewModel
{
    public sealed class AdminViewModel : CommonViewModel
    {
        private readonly IAdministrator _administrator;

        private ICommand _startStopCommand;

        public AdminViewModel([NotNull] IAdministrator administrator)
        {
            if (administrator == null) throw new ArgumentNullException("administrator");

            _administrator = administrator;
            BankId = Guid.NewGuid();
            BankName = "Bank Name";
        }

        public Guid BankId { get; private set; }
        public string BankName { get; set; }
        public bool Started { get { return _administrator.OfficeIsOpened; } }
        public string Status { get { return Started ? "Opened" : "Closed"; } }

        public ICommand StartStopCommand
        {
            get { return _startStopCommand ?? (_startStopCommand = new DelegateCommand(ExecuteStartStopCommand)); }
        }

        private void ExecuteStartStopCommand()
        {
            if (_administrator.OfficeIsOpened)
            {
                _administrator.CloseOffice();
            }
            else
            {
                _administrator.OpenOffice();
            }
            OnPropertyChanged(()=>Status);
        }
    }
}
