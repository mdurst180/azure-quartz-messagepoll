using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.WindowsAzure.StorageClient;
using PollingWorkerRole.Helpers;
using PollingWorkerRole.Models;
using Quartz;

namespace PollingWorkerRole.Jobs
{
    public class MyQueueJob : IJob
    {
        private ContainerBuilder builder;

        public MyQueueJob()
        {
            builder = new ContainerBuilder();
            this.InitContainer();
        }

        private void InitContainer()
        {
            //builder.RegisterType<EnrollmentService>().As<IEnrollmentService>();

            IQueue<CloudQueueMessage> queue = new AzureCloudQueue("MyQueue");
            builder.RegisterInstance(queue).AsSelf().Named<IQueue<CloudQueueMessage>>("CloudQueue");

            IQueue<CloudQueueMessage> errorQueue = new AzureCloudQueue("MyQueueError");
            builder.RegisterInstance(errorQueue).AsSelf().Named<IQueue<CloudQueueMessage>>("CloudErrorQueue");
        }

        public void Execute(IJobExecutionContext context)
        {
            using (IContainer container = builder.Build())
            {
                MyQueueProcessor processor = new MyQueueProcessor(container);
                processor.ProcessQueue();
            }
        }
    }
}
