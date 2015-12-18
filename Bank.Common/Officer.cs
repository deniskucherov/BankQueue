using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common
{
    public sealed class Officer
    {
        public Officer(Person person)
        {
            Person = person;
        }

        public Person Person { get; private set; }
    }
}
