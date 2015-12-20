using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Value
{
    public struct Stamp : IEquatable<Stamp>
    {
        public Stamp(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        public bool Equals(Stamp other)
        {
            return Id.Equals(other.Id);
        }
    }
}
