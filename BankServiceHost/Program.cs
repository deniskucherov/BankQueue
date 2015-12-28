using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankService;

namespace BankServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0} --> starting...", DateTime.Now);

            using (var m = new Mutex(true, @"Global\BankServiceHost"))
            {
                var allowed = m.WaitOne(TimeSpan.FromSeconds(1));
                if (!allowed)
                {
                    Console.WriteLine("Can't start duplicate service host.");
                    Console.ReadLine();
                    return;
                }

                try
                {
                    HostService();
                }
                finally
                {
                    m.ReleaseMutex();
                }
            }
        }

        private static void HostService()
        {
            var services = new List<ServiceHost>()
            {
                new ServiceHost(typeof (DataServiceManager)),
                new ServiceHost(typeof (ReportServiceManager))
            };

            try
            {
                foreach (var service in services)
                {
                    service.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                return;
            }

            Console.WriteLine("{0} --> BANK SERVICE IS RUNNING ...", DateTime.Now);
            Console.ReadLine();

            Console.WriteLine("Service closing...");
            services.ForEach(x => x.Close());
        }
    }
}
