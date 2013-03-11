using System;
using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
    [ConfigurationCollection(typeof(QuartzSecondlyJobConfigurationElement),
        AddItemName="secondlyJob",
        CollectionType=ConfigurationElementCollectionType.BasicMap)]
    public class QuartzSecondlyJobConfigurationElementCollection
        : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection properties;

        static QuartzSecondlyJobConfigurationElementCollection()
        {
            properties = new ConfigurationPropertyCollection();
        }

        public QuartzSecondlyJobConfigurationElementCollection()
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

        public QuartzSecondlyJobConfigurationElement this[int index]
        {
            get
            {
                return (QuartzSecondlyJobConfigurationElement)base.BaseGet(index);
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

        new public QuartzSecondlyJobConfigurationElement this[string name]
        {
            get
            {
                return (QuartzSecondlyJobConfigurationElement)base.BaseGet(name);
            }
        }

        public QuartzSecondlyJobConfigurationElement this[Type jobType]
        {
            get
            {
                return (QuartzSecondlyJobConfigurationElement)base.BaseGet(jobType);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new QuartzSecondlyJobConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as QuartzSecondlyJobConfigurationElement).JobType;
        }

        protected override string ElementName
        {
            get
            {
                return "secondlyJob";
            }
        }
    }
}
