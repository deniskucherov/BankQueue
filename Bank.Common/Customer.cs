using Bank.Common.Value;

namespace Bank.Common
{
    public class Customer
    {
        public Customer(string id, Age age, Gender sex)
        {
            Id = id;
            Age = age;
            Sex = sex;
        }

        public string Id { get; private set; }
        public Age Age { get; private set; } 
        public Gender Sex { get; private set; }
    }
}
