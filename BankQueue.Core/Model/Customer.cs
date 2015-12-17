namespace BankQueue.Core.Model
{
    public class Customer
    {
        public Customer(string id, int age, Gender sex)
        {
            Id = id;
            Age = age;
            Sex = sex;
        }

        public enum  Gender
        {
            M,
            F
        }

        public string Id { get; private set; }
        public int Age { get; private set; } 
        public Gender Sex { get; private set; }
    }
}
