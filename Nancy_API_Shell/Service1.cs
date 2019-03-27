using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Nancy_API_Shell
{
    public partial class Service1 : ServiceBase
    {
        public NancyHost nancyHost = new NancyHost(new Uri("http://localhost:9653"), new CustomNancyBootstrapper());

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            nancyHost.Start();
        }

        protected override void OnStop()
        {
            nancyHost.Stop();
        }
    }
}
