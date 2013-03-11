using System;
using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
    [ConfigurationCollection(typeof(QuartzDailyJobConfigurationElement),
        AddItemName="dailyJob",
        CollectionType=ConfigurationElementCollectionType.BasicMap)]
    public class QuartzDailyJobConfigurationElementCollection
        : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection properties;

        static QuartzDailyJobConfigurationElementCollection()
        {
            properties = new ConfigurationPropertyCollection();
        }

        public QuartzDailyJobConfigurationElementCollection()
        {
            //does nothing ... yet
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return properties;
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        public QuartzDailyJobConfigurationElement this[int index]
        {
            get
            {
                return (QuartzDailyJobConfigurationElement)base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }

        new public QuartzDailyJobConfigurationElement this[string name]
        {
            get
            {
                return (QuartzDailyJobConfigurationElement)base.BaseGet(name);
            }
        }

        public QuartzDailyJobConfigurationElement this[Type jobType]
        {
            get
            {
                return (QuartzDailyJobConfigurationElement)base.BaseGet(jobType);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new QuartzDailyJobConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
			QuartzDailyJobConfigurationElement e = element as QuartzDailyJobConfigurationElement;
			return new Tuple<Type, int, int> ( e.JobType, e.Hour, e.Minute );
        }

        protected override string ElementName
        {
            get
            {
                return "dailyJob";
            }
        }
    }
}
