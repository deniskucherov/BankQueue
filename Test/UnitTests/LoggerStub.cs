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
            
        }

        public void LogInformation(string message)
        {
            
        }

        public void LogTrace(string message)
        {
            
        }
    }
}
