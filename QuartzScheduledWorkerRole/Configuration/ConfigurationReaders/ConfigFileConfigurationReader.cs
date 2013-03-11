using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using Quartz.Impl;

namespace QuartzScheduledWorkerRole.Configuration.ConfigurationReaders
{
	public class ConfigFileConfigurationReader : IConfigurationReader
	{
		private readonly QuartzJobConfigurationSection section;

		public ConfigFileConfigurationReader ( QuartzJobConfigurationSection section )
		{
			this.section = section;
		}

		public IEnumerable<KeyValuePair<IJobDetail, ITrigger>> ReadTriggers ( )
		{
			return this.ReadTriggers ( this.section.SecondlyJobs )
				.Concat ( this.ReadTriggers ( this.section.DailyJobs ) )
				.Concat ( this.ReadTriggers ( this.section.WeeklyJobs ) );
		}

        private IEnumerable<KeyValuePair<IJobDetail, ITrigger>> ReadTriggers( QuartzWeeklyJobConfigurationElementCollection weeklyJobs )
		{
			return weeklyJobs.OfType<QuartzWeeklyJobConfigurationElement> ( )
				.Select ( MakeTrigger );
		}

        private KeyValuePair<IJobDetail, ITrigger> MakeTrigger( QuartzWeeklyJobConfigurationElement weeklyJob )
		{
            IJobDetail weeklyJobDetail =
                new JobDetailImpl( weeklyJob.JobType.Name + "." + weeklyJob.DayOfWeek + "." + weeklyJob.Hour, weeklyJob.JobType );

            int day = Convert.ToInt32(weeklyJob.DayOfWeek) + 1;

            ITrigger trigger2 = TriggerBuilder.Create()
                                              .WithDailyTimeIntervalSchedule
                ( s =>
                 s.WithIntervalInHours( 24 )
                  .OnDaysOfTheWeek( weeklyJob.DayOfWeek )
                  .StartingDailyAt( TimeOfDay.HourAndMinuteOfDay( weeklyJob.Hour, weeklyJob.Minute ) )
                ).Build();

            return new KeyValuePair<IJobDetail, ITrigger>( weeklyJobDetail, trigger2 );
		}

        private IEnumerable<KeyValuePair<IJobDetail, ITrigger>> ReadTriggers( QuartzDailyJobConfigurationElementCollection dailyJobs )
		{
			return dailyJobs.OfType<QuartzDailyJobConfigurationElement> ( )
				.Select ( MakeTrigger );
		}

        private KeyValuePair<IJobDetail, ITrigger> MakeTrigger( QuartzDailyJobConfigurationElement dailyJob )
		{
			string dailyJobName = string.Format (
				"{0}.{1:00}{2:00}.Trigger",
				dailyJob.JobType.Name,
                dailyJob.Hour,
                dailyJob.Minute );

            IJobDetail jobDetail =
                new JobDetailImpl( dailyJobName, dailyJob.JobType );

            ITrigger trigger2 = TriggerBuilder.Create()
                                              .WithDailyTimeIntervalSchedule
                (s =>
                 s.WithIntervalInHours(24)
                  .OnEveryDay()
                  .StartingDailyAt( TimeOfDay.HourAndMinuteOfDay( dailyJob.Hour, dailyJob.Minute ) )
                ).Build();

            return new KeyValuePair<IJobDetail, ITrigger>( jobDetail, trigger2 );
		}

        private IEnumerable<KeyValuePair<IJobDetail, ITrigger>> ReadTriggers( QuartzSecondlyJobConfigurationElementCollection secondlyJobs )
		{
			return secondlyJobs.OfType<QuartzSecondlyJobConfigurationElement> ( )
				.Select ( MakeTrigger );
		}

        private KeyValuePair<IJobDetail, ITrigger> MakeTrigger( QuartzSecondlyJobConfigurationElement secondlyJob )
		{
            string triggerName = secondlyJob.JobType.Name + "Trigger";
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity( triggerName, "group1" )
                .WithSimpleSchedule( x => x.WithIntervalInSeconds( secondlyJob.TriggerInterval ).RepeatForever() )
                .Build();
            trigger.Priority = secondlyJob.TriggerPriority;
            IJobDetail jobDetail = new JobDetailImpl( secondlyJob.JobType.Name, secondlyJob.JobType );

            return new KeyValuePair<IJobDetail, ITrigger>( jobDetail, trigger );
		}
	}
}
