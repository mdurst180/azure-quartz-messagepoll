using System.Diagnostics;
using Quartz;

namespace QuartzScheduledWorkerRole.Jobs
{
    class DailyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Trace.WriteLine( "Hello Daily" );
        }
    }
}
