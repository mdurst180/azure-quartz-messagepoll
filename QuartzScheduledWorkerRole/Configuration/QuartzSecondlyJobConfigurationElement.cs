using System;
using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
	public class QuartzSecondlyJobConfigurationElement : ConfigurationElement
	{
		private static ConfigurationProperty jobType;
		private static ConfigurationProperty triggerInterval;
        private static ConfigurationProperty triggerPriority;
        private static ConfigurationProperty scheduler;

		private static ConfigurationPropertyCollection properties;

		static QuartzSecondlyJobConfigurationElement ( )
		{
			jobType = new ConfigurationProperty (
				"jobType",
				typeof ( Type ),
				null,
				new TypeNameConverter(),
				null,
				ConfigurationPropertyOptions.IsRequired );


			triggerInterval = new ConfigurationProperty (
				"triggerInterval",
				typeof ( int ),
				null,
				ConfigurationPropertyOptions.None );

            // Priority controls which trigger fires first when multiple waiting for a thread
            triggerPriority = new ConfigurationProperty(
                "triggerPriority",
                typeof( int ),
                5,
                ConfigurationPropertyOptions.None );

            scheduler = new ConfigurationProperty(
                "scheduler",
                typeof( string ),
                null,
                ConfigurationPropertyOptions.None );

			properties = new ConfigurationPropertyCollection ( );
			properties.Add( jobType );
			properties.Add( triggerInterval );
            properties.Add( triggerPriority );
            properties.Add( scheduler );
		}

		public Type JobType
		{
			get
			{
				return ( Type ) base [ "jobType" ];
			}
		}

		public int TriggerInterval
		{
			get
			{
				return ( int ) base [ "triggerInterval" ];
			}
		}

        public int TriggerPriority
        {
            get
            {
                return (int)base["triggerPriority"];
            }
        }

        [ConfigurationProperty( "useDefaultScheduler" )]
        public bool UseDefaultScheduler
        {
            get
            {
                return true;
            }
        }

        [ConfigurationProperty( "scheduler" )]
        public string Scheduler
        {
            get
            {
                return (string)base["scheduler"];
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
