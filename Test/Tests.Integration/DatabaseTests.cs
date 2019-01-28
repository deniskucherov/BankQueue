using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests.Integration
{
    [TestFixture]
    public class DatabaseTests
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestConnection(bool result)
        {
            Assert.IsTrue(result);
        }
    }
}
