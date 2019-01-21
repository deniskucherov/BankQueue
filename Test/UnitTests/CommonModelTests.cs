using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Common;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class CommonModelTests
    {
        [Test]
        [TestCase("Ivan", 20, Gender.M)]
        public void Person(string name, int age, Gender sex)
        {
            var person = new Person(name, age, sex);

            Assert.AreEqual(name, person.Name);
            Assert.AreEqual(age, person.Age);
            Assert.AreEqual(sex, person.Sex);
        }
    }
}
