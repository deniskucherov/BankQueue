using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Bank.Common.Interface;
using Bank.Common.Value;
using BankQueue.Core.Annotations;

namespace BankQueue.Core
{
    public sealed class CashierDesk : IStampProvider, INotifyPropertyChanged
    {
        private readonly object _syncRoot = new object();
        private readonly Semaphore _stampSemaphore;
        private readonly List<CashierStampRecord> _stamps; 

        private int _stampsRemains; 
        
        public CashierDesk()
        {
            _stampSemaphore = new Semaphore(StampsCount, StampsCount);
            _stamps = Enumerable.Range(0, StampsCount).Select(x => new CashierStampRecord(new Stamp(string.Format("Stamp #{0}", x + 1)))).ToList();
            _stampsRemains = StampsCount;
        }

        public int StampsCount { get { return 10; } }

        public int StampsRemains
        {
            get { return _stampsRemains; }
            private set
            {
                _stampsRemains = value;
                OnPropertyChanged();
            }
        }

        public Stamp GetStamp([NotNull] IOfficer officer)
        {
            try
            {
                _stampSemaphore.WaitOne();
                lock (_syncRoot)
                {
                    var stampRecord = _stamps.FirstOrDefault(x => x.Status == CashierStampRecord.StampStatus.Free);
                    if (stampRecord == null)
                        throw new ApplicationException("GetStamp error. stampRecord == null");
                    stampRecord.ChangeStatus(CashierStampRecord.StampStatus.Issued, officer);
                    
                    StampsRemains--;
                    return stampRecord.Stamp;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("GetStamp error.", ex);
            }
        }

        public void ReturnStamp([NotNull] IOfficer officer)
        {
            try
            {
                lock (_syncRoot)
                {
                    var stampRecord = _stamps.SingleOrDefault(x => x.Status == CashierStampRecord.StampStatus.Issued
                                                                   && x.Officer == officer);
                    if (stampRecord == null)
                        throw new ApplicationException("ReturnStamp error. stampRecord == null");

                    stampRecord.ChangeStatus(CashierStampRecord.StampStatus.Free, officer);
                    StampsRemains++;
                }

                _stampSemaphore.Release();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ReturnStamp", ex);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
