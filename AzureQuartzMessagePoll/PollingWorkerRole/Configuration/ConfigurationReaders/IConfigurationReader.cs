using System.Collections.Generic;
using Quartz;

namespace PollingWorkerRole.Configuration.ConfigurationReaders
{
	public interface IConfigurationReader
	{
        IEnumerable<KeyValuePair<IJobDetail, ITrigger>> ReadTriggers();
	}
}
