using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
	public class QuartzJobConfigurationSection : ConfigurationSection
	{

		private static ConfigurationProperty secondlyJobs;
		private static ConfigurationProperty dailyJobs;
		private static ConfigurationProperty weeklyJobs;

		private static ConfigurationPropertyCollection properties;

		static QuartzJobConfigurationSection ( )
		{
			secondlyJobs = new ConfigurationProperty (
				"secondlyJobs",
				typeof ( QuartzSecondlyJobConfigurationElementCollection ),
				null,
				ConfigurationPropertyOptions.None );

			dailyJobs = new ConfigurationProperty (
				"dailyJobs",
				typeof ( QuartzDailyJobConfigurationElementCollection ),
				null,
				ConfigurationPropertyOptions.None );

			weeklyJobs = new ConfigurationProperty (
				"weeklyJobs",
				typeof ( QuartzWeeklyJobConfigurationElementCollection ),
				null,
				ConfigurationPropertyOptions.None );
			
			properties = new ConfigurationPropertyCollection ( );
			properties.Add ( secondlyJobs );
            properties.Add( dailyJobs );
            properties.Add( weeklyJobs );
		}

		public QuartzSecondlyJobConfigurationElementCollection SecondlyJobs
		{
			get
			{
				return ( QuartzSecondlyJobConfigurationElementCollection ) base [ "secondlyJobs" ];
			}
		}

		public QuartzDailyJobConfigurationElementCollection DailyJobs
		{
			get
			{
				return ( QuartzDailyJobConfigurationElementCollection ) base [ "dailyJobs" ];
			}
		}

		public QuartzWeeklyJobConfigurationElementCollection WeeklyJobs
		{
			get
			{
				return ( QuartzWeeklyJobConfigurationElementCollection ) base [ "weeklyJobs" ];
			}
		}
	
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return properties;
			}
		}
	}
}
