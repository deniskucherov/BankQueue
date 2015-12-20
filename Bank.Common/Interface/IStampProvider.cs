using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Value;

namespace Bank.Common.Interface
{
    public interface IStampProvider
    {
        int StampsCount { get; }
        int StampsRemains { get; }

        Stamp GetStamp(IOfficer officer);
        void ReturnStamp(IOfficer officer);
    }
}
