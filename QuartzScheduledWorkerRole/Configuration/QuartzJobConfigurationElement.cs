using System;
using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
	public class QuartzJobConfigurationElement : ConfigurationElement
	{
        protected static ConfigurationProperty jobType;

        protected static ConfigurationPropertyCollection properties;

        static QuartzJobConfigurationElement()
		{
			jobType = new ConfigurationProperty (
                "jobType",
                typeof( Type ),
                null,
                new TypeNameConverter(),
                null,
                ConfigurationPropertyOptions.IsRequired );
            properties = new ConfigurationPropertyCollection { jobType };
		}

        [ConfigurationProperty( "jobType" )]
        public Type JobType
        {
            get
            {
                return (Type)base["jobType"];
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
