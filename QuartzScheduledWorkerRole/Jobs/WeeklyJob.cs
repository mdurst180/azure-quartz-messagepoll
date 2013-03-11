using System.Diagnostics;
using Quartz;

namespace QuartzScheduledWorkerRole.Jobs
{
    class WeeklyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Trace.WriteLine("Hello Weekly");
        }
    }
}
