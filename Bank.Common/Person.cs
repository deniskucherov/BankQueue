using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common
{
    public struct Person
    {
        public Person(string name, int age, Gender sex) : this()
        {
            Name = name;
            Age = age;
            Sex = sex;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public Gender Sex { get; private set; }
    }
}
