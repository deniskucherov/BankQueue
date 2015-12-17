using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace BankQueue.ViewModel
{
    public sealed class AdminViewModel : CommonViewModel
    {
        private ICommand _startStopCommand;

        public AdminViewModel()
        {
            
        }

        public ICommand StartStopCommand
        {
            get { return _startStopCommand ?? (_startStopCommand = new DelegateCommand(ExecuteStartStopCommand)); }
        }

        public bool Started { get; private set; }

        private void ExecuteStartStopCommand()
        {
            Started = !Started;
        }
    }
}
