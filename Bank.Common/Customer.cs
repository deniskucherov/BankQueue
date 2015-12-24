using Bank.Common.Value;

namespace Bank.Common
{
    public class Customer
    {
        public Customer(string name, Age age, Gender sex)
        {
            Name = name;
            Age = age;
            Sex = sex;
        }

        public string Name { get; private set; }
        public Age Age { get; private set; } 
        public Gender Sex { get; private set; }
    }
}
