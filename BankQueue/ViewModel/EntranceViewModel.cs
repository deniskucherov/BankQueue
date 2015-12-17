using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using Bank.Common.Interface;

namespace BankQueue.ViewModel
{
    public sealed class EntranceViewModel : CommonServiceViewModel
    {
        private readonly IEntranceDemon _entranceDemon;

        public EntranceViewModel(IEntranceDemon entranceDemon)
        {
            if (entranceDemon == null) throw new ArgumentNullException(nameof(entranceDemon));
            _entranceDemon = entranceDemon;
        }

        protected override void ExecuteStartCommand()
        {
            _entranceDemon.Start();
        }

        protected override void ExecuteStopCommad()
        {
            _entranceDemon.Stop();
        }
    }
}
