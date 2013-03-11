using System.Collections.Generic;
using System.Configuration;
using Quartz;
using Quartz.Impl;
using QuartzScheduledWorkerRole.Configuration;
using QuartzScheduledWorkerRole.Configuration.ConfigurationReaders;

namespace QuartzScheduledWorkerRole
{
	/// <summary>
	/// Class for managing the Quartz.NET configuration
	/// </summary>
	internal class QuartzManager
	{
		private const int ThreadCount = 1;
		private static readonly object lockObject = new object();

		private static bool isConfigured;

		private readonly IConfigurationReader reader;

		internal QuartzManager ()
		{
            var quartzJobsSection = ConfigurationManager.GetSection( "quartzJobs" ) as QuartzJobConfigurationSection;
            reader = new ConfigFileConfigurationReader( quartzJobsSection );
		}

		/// <summary>
		/// Configures the Quartz Scheduler.
		/// </summary>
		/// <returns></returns>
		public void Configure ( )
		{
			if ( isConfigured )
			{
				return;
			}
			lock ( lockObject )
			{
                ScheduleJobs();
			}
		    StartJobs();

		    isConfigured = true;
		}

        /// <summary>
        /// Starts the jobs.
        /// </summary>
	    private static void StartJobs()
	    {
            DirectSchedulerFactory.Instance.GetScheduler().Start();
	    }
	    /// <summary>
        /// Schedules the jobs.
        /// </summary>
		private void ScheduleJobs ( )
	    {
            DirectSchedulerFactory.Instance.CreateVolatileScheduler( ThreadCount );

            foreach ( KeyValuePair<IJobDetail, ITrigger> jobInfo in reader.ReadTriggers() )
			{
			    IJobDetail job = jobInfo.Key;
                DirectSchedulerFactory.Instance.GetScheduler().ScheduleJob( job, jobInfo.Value );
			}
		}
        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        public void Shutdown()
        {
            DirectSchedulerFactory.Instance.GetScheduler().Shutdown();
        }
	}
}
