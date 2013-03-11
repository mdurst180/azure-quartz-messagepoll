using System;
using System.Diagnostics;
using Autofac;
using Microsoft.WindowsAzure.StorageClient;
using PollingWorkerRole.Helpers;
using PollingWorkerRole.Models;
using Quartz;

namespace PollingWorkerRole.Jobs
{
    public abstract class BaseQueueProcessor<TMessagePayloadType> : IJob
    {
        private readonly IQueue<CloudQueueMessage> cloudQueue;
        private readonly IQueue<CloudQueueMessage> cloudErrorQueue;

        protected BaseQueueProcessor()
        {
        }

        /// <summary>
        /// Initializes a BaseQueueProcessor object with an instance of a queue
        /// and the visibility timeout for messages retrieved from the queue.
        /// </summary>
        /// <param name="container">The container.</param>
        /// representing the interval at which messages will become available
        /// for reading after having been dequeued
        protected BaseQueueProcessor(IContainer container)
        {
            this.cloudQueue = container.ResolveNamed<IQueue<CloudQueueMessage>>("CloudQueue");
            this.cloudErrorQueue = container.ResolveNamed<IQueue<CloudQueueMessage>>("CloudErrorQueue");
        }


        /// <summary>
        ///   Executes the specified context.
        /// </summary>
        /// <param name = "context">The context.</param>
        public void Execute( IJobExecutionContext context )
        {
            try
            {
                AddFakeMessage();
                this.ProcessQueue();
            }
            catch (Exception e)
            {
                Trace.WriteLine(String.Format("{0}: Unexpected failure while processing Queue: {1}. Error: {2}",
                                              this.GetType(), cloudQueue.Name, e.Message));
            }
        }

        private void AddFakeMessage()
        {
            Payload myMessage = new Payload();
            myMessage.SomeString = "Some Fake Message";
            myMessage.SomeId = 10;
            this.cloudQueue.Put( new CloudQueueMessage( myMessage.ToJson() ) );

        }

        /// <summary>
        /// Processes the queue.
        /// </summary>
        public void ProcessQueue()
        {
            var ticks = new TimeSpan(1000);

            CloudQueueMessage message = this.cloudQueue.Get( ticks );
            if (message != null)
            {
                try
                {
                    var messagePayload = message.GetMessagePayload<TMessagePayloadType>();
                    if (this.ProcessMessage(messagePayload))
                    {
                        this.cloudQueue.Delete(message);
                    }
                }
                catch (Exception ex)
                {
                    if (!ShouldRetry())
                    {
                        MoveToDeadLetterQueue(message, ex);
                    }
                    Trace.WriteLine(ex.ToString());
                }
            }
        }

        private bool ShouldRetry()
        {
            return false;
        }

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="messagePayload">The message payload.</param>
        /// <returns></returns>
        protected abstract bool ProcessMessage(TMessagePayloadType messagePayload);

        /// <summary>
        /// Moves to dead letter queue.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        private void MoveToDeadLetterQueue(CloudQueueMessage message, Exception ex)
        {
            this.cloudQueue.Delete(message);
            this.cloudErrorQueue.Put(message);
        }
    }
}
