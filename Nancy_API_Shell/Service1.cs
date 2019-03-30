using Nancy.Hosting.Self;
using System;
using System.ServiceProcess;

/*  I wrote this as a windows service as a way to 
 *  utilize microservices
 *  
 *  You can split up different APIs into 
 *  different services and control each one via
 *  Windows Services.
 * 
 * */

namespace Nancy_API_Shell
{
    public partial class Service1 : ServiceBase
    {
        //Create a new self hosted Nancy Host w/ Custom Boot Strapper for pipeline control
        public static string url = "http://localhost:1234";
        public NancyHost nancyHost = new NancyHost(new Uri(url), new CustomNancyBootstrapper());

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            nancyHost.Start();
            Console.WriteLine("Nancy Server listening on { 0 }", url);
        }

        protected override void OnStop()
        {
            nancyHost.Stop();
            Console.WriteLine("Nancy Server stopped");
        }
    }
}
