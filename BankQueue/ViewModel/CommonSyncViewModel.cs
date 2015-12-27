using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankQueue.ViewModel
{
    public abstract class CommonSyncViewModel : CommonViewModel
    {
        protected readonly object _syncRoot;
        protected readonly SynchronizationContext _syncContext;

        public CommonSyncViewModel()
        {
            _syncRoot = new object();
            _syncContext = SynchronizationContext.Current;
        }
    }
}
