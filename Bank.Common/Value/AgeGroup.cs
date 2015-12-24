using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Common.Value
{
    public abstract class AgeGroup
    {
        public AgeGroup(int age)
        {
            Age = age;
        }

        public int Age { get; private set; }

        public abstract double AgeFactor();

        public static AgeGroup Create(int age)
        {
            if (age < 16)
                throw new ApplicationException("age < 16");

            AgeGroup result = null;
            if (age <= 26)
            {
                result = new AgeGroupA(age);
            } 
            else if (age > 25 && age < 36)
            {
                result = new AgeGroupB(age);
            } 
            else if (age > 35 && age < 46)
            {
                result = new AgeGroupC(age);
            } 
            else if (age > 45 && age < 61)
            {
                result = new AgeGroupD(age);
            } 
            else if (age > 60 && age < 151)
            {
                result = new AgeGroupE(age);
            }
            else
            {
                throw new ApplicationException("He is too old! May be he's a vampire! :-0");
            }

            return result;
        }
    }

    /// <summary>
    /// 16-25
    /// </summary>
    public class AgeGroupA : AgeGroup
    {
        public AgeGroupA(int age) : base(age)
        {
        }

        public override double AgeFactor()
        {
            return 0.2;
        }
    }

    /// <summary>
    /// 26-35
    /// </summary>
    public class AgeGroupB : AgeGroup
    {
        public AgeGroupB(int age) : base(age)
        {
        }

        public override double AgeFactor()
        {
            return 0.4;
        }
    }

    /// <summary>
    /// 36 - 45
    /// </summary>
    public class AgeGroupC : AgeGroup
    {
        public AgeGroupC(int age) : base(age)
        {
        }

        public override double AgeFactor()
        {
            return 0.6;
        }
    }

    /// <summary>
    /// 46 - 60
    /// </summary>
    public class AgeGroupD : AgeGroup
    {
        public AgeGroupD(int age) : base(age)
        {

        }

        public override double AgeFactor()
        {
            return 0.8;
        }
    }

    /// <summary>
    /// 61 - 150
    /// </summary>
    public class AgeGroupE : AgeGroup
    {
        public AgeGroupE(int age) : base(age)
        {
        }

        public override double AgeFactor()
        {
            return 1;
        }
    }
}
