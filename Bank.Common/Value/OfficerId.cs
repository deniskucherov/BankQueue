using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Value
{
    public struct OfficerId : IEquatable<OfficerId>
    {
        public OfficerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; private set; }

        public bool Equals(OfficerId other)
        {
            return Value.Equals(other.Value);
        }
    }
}
