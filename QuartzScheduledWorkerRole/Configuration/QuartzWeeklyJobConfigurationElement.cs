using System;
using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
    public class QuartzWeeklyJobConfigurationElement : ConfigurationElement
	{
        private static ConfigurationProperty jobType;
		private static ConfigurationProperty dayOfWeek;
		private static ConfigurationProperty hour;
		private static ConfigurationProperty minute;

		private static ConfigurationPropertyCollection properties;

		static QuartzWeeklyJobConfigurationElement ( )
		{
            jobType = new ConfigurationProperty(
                "jobType",
                typeof( Type ),
                null,
                new TypeNameConverter(),
                null,
                ConfigurationPropertyOptions.IsRequired );

			dayOfWeek = new ConfigurationProperty (
				"dayOfWeek",
				typeof ( DayOfWeek ),
				null,
				ConfigurationPropertyOptions.IsRequired );

			hour = new ConfigurationProperty (
				"hour",
				typeof ( int ),
				null,
				ConfigurationPropertyOptions.IsRequired );

			minute = new ConfigurationProperty (
				"minute",
				typeof ( int ),
				null,
				ConfigurationPropertyOptions.IsRequired );

            properties = new ConfigurationPropertyCollection();
            properties.Add( jobType );
			properties.Add ( dayOfWeek );
			properties.Add ( hour );
			properties.Add ( minute );
		}

        [ConfigurationProperty( "jobType" )]
        public Type JobType
        {
            get
            {
                return (Type)base["jobType"];
            }
        }

		[ConfigurationProperty ( "dayOfWeek" )]
		public DayOfWeek DayOfWeek
		{
			get
			{
				return ( DayOfWeek ) base [ "dayOfWeek" ];
			}
		}
		
		[ConfigurationProperty ( "hour" )]
		public int Hour
		{
			get
			{
				return ( int ) base [ "hour" ];
			}
		}

		[ConfigurationProperty ( "minute" )]
		public int Minute
		{
			get
			{
				return ( int ) base [ "minute" ];
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
