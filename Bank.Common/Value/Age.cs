using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Value
{
    public struct Age
    {
        public Age(int value, AgeGroup ageGroup) : this()
        {
            Value = value;
            AgeGroup = ageGroup;
        }

        public int Value { get; private set; }
        public AgeGroup AgeGroup { get; private set; }

        public static Age Create(int age)
        {
            return new Age(age, AgeGroup.Create(age));
        }
    }
}
