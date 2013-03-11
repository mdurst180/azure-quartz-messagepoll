using System;
using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
    [ConfigurationCollection(typeof(QuartzWeeklyJobConfigurationElement),
        AddItemName="weeklyJob",
        CollectionType=ConfigurationElementCollectionType.BasicMap)]
    public class QuartzWeeklyJobConfigurationElementCollection
        : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection properties;

        static QuartzWeeklyJobConfigurationElementCollection()
        {
            properties = new ConfigurationPropertyCollection();
        }

        public QuartzWeeklyJobConfigurationElementCollection()
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

        public QuartzWeeklyJobConfigurationElement this[int index]
        {
            get
            {
                return (QuartzWeeklyJobConfigurationElement)base.BaseGet(index);
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

        new public QuartzWeeklyJobConfigurationElement this[string name]
        {
            get
            {
                return (QuartzWeeklyJobConfigurationElement)base.BaseGet(name);
            }
        }

        public QuartzWeeklyJobConfigurationElement this[Type jobType]
        {
            get
            {
                return (QuartzWeeklyJobConfigurationElement)base.BaseGet(jobType);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new QuartzWeeklyJobConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as QuartzWeeklyJobConfigurationElement).JobType;
        }

        protected override string ElementName
        {
            get
            {
                return "weeklyJob";
            }
        }
    }
}
