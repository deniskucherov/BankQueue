using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketDog.Common;

namespace UnitTests
{
    class LoggerStub : ILogger
    {
        public void LogError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(string message)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string message)
        {
            throw new NotImplementedException();
        }
    }
}
