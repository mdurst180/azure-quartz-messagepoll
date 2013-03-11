using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.StorageClient;
using PollingWorkerRole.Models;

namespace PollingWorkerRole.Helpers
{
    public class AzureCloudQueue : IQueue<CloudQueueMessage>
    {
        private readonly CloudQueue cloudQueue;
        public AzureCloudQueue( string queueName )
        {
            cloudQueue = AzureQueueHelper.GetQueue( queueName );
        }

        public string Name
        {
            get { return cloudQueue.Name; }
        }

        public CloudQueueMessage Get( TimeSpan visibilityTimeout )
        {
            return cloudQueue.GetMessage( visibilityTimeout );
        }

        public void Put( CloudQueueMessage queuePayload )
        {
            cloudQueue.AddMessage( queuePayload );
        }

        public void Delete( CloudQueueMessage queuePayload )
        {
            cloudQueue.DeleteMessage( queuePayload );
        }
    }
}
