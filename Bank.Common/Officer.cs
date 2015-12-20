using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common.Interface;
using Bank.Common.Value;

namespace Bank.Common
{
    public sealed class Officer : IOfficer, IEquatable<Officer>
    {
        private readonly OfficerId _id;

        public Officer(Person person)
        {
            _id = new OfficerId(Guid.NewGuid());
            Person = person;
        }

        public Person Person { get; private set; }
        public OfficerId Id { get { return _id; } }

        public bool Equals(Officer other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Id.Equals(other.Id);
        }   
    }
}
