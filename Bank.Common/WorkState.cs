using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common
{
    public enum WorkState
    {
        NaN = 0,
        InWork = 1,
        Stoped = 2,
        Paused = 3,
        Completed = 4,
        Rejected = 5
    }
}
