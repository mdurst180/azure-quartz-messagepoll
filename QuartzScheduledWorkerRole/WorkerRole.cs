using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace QuartzScheduledWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private QuartzManager manager;
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine( "QuartzScheduledWorkerRole entry point called", "Information" );

            while ( true )
            {
                Thread.Sleep( 10000 );
                Trace.WriteLine( "Working", "Information" );
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
            manager = new QuartzManager();
            manager.Configure();
            
            return base.OnStart();
        }
    }
}
