using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace BankQueue.ViewModel
{
    public abstract class CommonServiceViewModel : CommonViewModel
    {
        private ICommand _startCommand, _stopCommand;

        public bool IsStarted { get; protected set; }

        public ICommand StartCommand
        {
            get { return _startCommand ?? (_startCommand = new DelegateCommand(ExecuteStartCommand, CanExecuteStartCommand)); }
        }

        public ICommand StopCommand
        {
            get { return _stopCommand ?? (_stopCommand = new DelegateCommand(ExecuteStopCommad, CanExecuteStopCommand)); }
        }

        protected virtual bool CanExecuteStopCommand()
        {
            return IsStarted;
        }

        protected virtual bool CanExecuteStartCommand()
        {
            return !IsStarted;
        }

        protected abstract void ExecuteStopCommad();
        protected abstract void ExecuteStartCommand();
    }
}
