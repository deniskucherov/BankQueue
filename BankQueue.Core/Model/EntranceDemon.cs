using System;
using System.Threading;
using Bank.Common;
using Bank.Common.Interface;

namespace BankQueue.Core.Model
{
    public sealed class EntranceDemon : IEntranceDemon
    {
        private readonly Timer _timer;

        public EntranceDemon()
        {
            _timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public IEntranceInformation Information { get; }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        private void Callback(object state)
        {
            
        }


    }
}
