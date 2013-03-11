using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace PollingWorkerRole.Helpers
{
    public static class AzureQueueHelper
    {
        private static readonly CloudQueueClient queueClient;

        static AzureQueueHelper()
        {
            //AzureConfigHelper.ConfigureStorageAccount();
            CloudStorageAccount.SetConfigurationSettingPublisher(
                ( configName, configSettingPublisher ) =>
                {
                    string connectionString = RoleEnvironment.GetConfigurationSettingValue( configName );
                    configSettingPublisher( connectionString );
                } );

            queueClient = CloudStorageAccount.FromConfigurationSetting( "QueueConnectionString" ).CreateCloudQueueClient();
        }

        /// <summary>
        /// Gets the specific queue reference.  Creates the queue if it doesn't exist.
        /// </summary>
        /// <param name = "queueName">Name of the queue.</param>
        /// <returns></returns>
        public static CloudQueue GetQueue( string queueName )
        {
            CloudQueue cloudQueue = null;
            try
            {
                cloudQueue = queueClient.GetQueueReference( queueName.ToLower() );
            }
            catch ( System.Net.WebException )
            {
                // Retry once if error received while connecting to queue
                cloudQueue = queueClient.GetQueueReference( queueName.ToLower() );
            }
            if ( cloudQueue.CreateIfNotExist() )
            {
                queueClient.RetryPolicy = RetryPolicies.Retry( 5, TimeSpan.FromHours( 2.0 ) );
            }
            return cloudQueue;
        }
    }
}
