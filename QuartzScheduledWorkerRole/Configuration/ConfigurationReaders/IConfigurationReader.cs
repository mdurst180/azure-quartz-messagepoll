using System.Collections.Generic;
using Quartz;

namespace QuartzScheduledWorkerRole.Configuration.ConfigurationReaders
{
	public interface IConfigurationReader
	{
        IEnumerable<KeyValuePair<IJobDetail, ITrigger>> ReadTriggers();
	}
}
