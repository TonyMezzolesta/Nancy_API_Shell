using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nancy_API_Shell_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9653";

            using (NancyHost host = new NancyHost(new Uri(url), new CustomNancyBootstrapper()))
            {
               host.Start();
               Console.WriteLine("Nancy Server listening on { 0 }", url);
               Console.ReadLine();
            }
        }
    }
}
