using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HostedWebProxyService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WebProxyService.ProxyService)))
            {
                host.Open();
                Console.WriteLine("The web proxy service is ready at {0}", host.BaseAddresses[0]);
                Console.ReadLine();
                // Close the ServiceHost - not really needed because Docker will destroy the host and us with it
                host.Close();
            }
        }
    }
}
