using System;
using System.Configuration;

namespace QuartzScheduledWorkerRole.Configuration
{
    [ConfigurationCollection(typeof(QuartzJobConfigurationElement),
        AddItemName="job",
        CollectionType=ConfigurationElementCollectionType.BasicMap)]
    public class QuartzJobConfigurationElementCollection
        : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection properties;

        static QuartzJobConfigurationElementCollection()
        {
            properties = new ConfigurationPropertyCollection();
        }

        public QuartzJobConfigurationElementCollection()
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

        public QuartzJobConfigurationElement this[int index]
        {
            get
            {
                return (QuartzJobConfigurationElement)base.BaseGet(index);
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

        new public QuartzJobConfigurationElement this[string name]
        {
            get
            {
                return (QuartzJobConfigurationElement)base.BaseGet(name);
            }
        }

        public QuartzJobConfigurationElement this[Type jobType]
        {
            get
            {
                return (QuartzJobConfigurationElement)base.BaseGet(jobType);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new QuartzJobConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as QuartzJobConfigurationElement).JobType;
        }

        protected override string ElementName
        {
            get
            {
                return "job";
            }
        }
    }
}
