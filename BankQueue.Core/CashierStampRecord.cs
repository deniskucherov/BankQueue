using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Interface;
using Bank.Common.Value;
using BankQueue.Core.Annotations;

namespace BankQueue.Core
{
    public sealed class CashierStampRecord
    {
        public CashierStampRecord(Stamp stamp)
        {
            Stamp = stamp;
            Status = StampStatus.Free;
            Timestamp = DateTime.Now;
        }

        public enum StampStatus
        {
            Free,
            Issued
        }

        public Stamp Stamp { get; private set; }   
        public StampStatus Status { get; private set; }
        public IOfficer Officer { get; private set; }
        public DateTime Timestamp { get; private set; }

        public void ChangeStatus(StampStatus status, [NotNull] IOfficer officer)
        {
            if (officer == null) throw new ArgumentNullException(nameof(officer));
            Status = status;
            Officer = officer;
            Timestamp = DateTime.Now;
        }
    }
}
