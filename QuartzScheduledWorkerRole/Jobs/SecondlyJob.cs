using System.Diagnostics;
using Quartz;

namespace QuartzScheduledWorkerRole.Jobs
{
    class SecondlyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Trace.WriteLine("Hello Secondly");
        }
    }
}
