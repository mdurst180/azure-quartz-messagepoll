using System;
using Autofac;
using Microsoft.WindowsAzure.StorageClient;
using Moq;
using NUnit.Framework;
using PollingWorkerRole.Jobs;
using PollingWorkerRole.Models;

namespace PollingWorkerRole.Tests
{
    [TestFixture]
    public class QueueTests
    {
        static Mock<IQueue<CloudQueueMessage>> mockQueue = new Mock<IQueue<CloudQueueMessage>>();
        static Mock<IQueue<CloudQueueMessage>> mockErrorQueue = new Mock<IQueue<CloudQueueMessage>>();
        private static MyQueueProcessor qProcessor;
        private static ContainerBuilder builder;

        [SetUp]
        public void Init()
        {
            builder = new ContainerBuilder();
            mockQueue = new Mock<IQueue<CloudQueueMessage>>();
            mockErrorQueue = new Mock<IQueue<CloudQueueMessage>>();
            builder.RegisterInstance( mockErrorQueue.Object ).AsSelf().Named<IQueue<CloudQueueMessage>>( "CloudErrorQueue" );
            builder.RegisterInstance( mockQueue.Object ).AsSelf().Named<IQueue<CloudQueueMessage>>( "CloudQueue" );
            var container = builder.Build();
            qProcessor = new MyQueueProcessor( container );
        }
        /// <summary>
        ///A test for BuildVerificationResponse
        ///</summary>
        [Test]
        public void TestProcessNullMessage()
        {
            qProcessor.ProcessQueue();
            mockQueue.Verify( x => x.Delete( It.IsAny<CloudQueueMessage>() ), Times.Never() );
        }

        /// <summary>
        ///A test for 
        ///</summary>
        [Test]
        public void TestProcessInvalidMessage()
        {
            mockQueue.Setup( x => x.Get( It.IsAny<TimeSpan>() ) ).Returns(
                new CloudQueueMessage( "{\"Some Junk Data\"}" ) );

            qProcessor.ProcessQueue();

            // Verify message gets moved to dead letter queue
            mockErrorQueue.Verify( x => x.Put( It.IsAny<CloudQueueMessage>() ), Times.Exactly( 1 ) );
            mockQueue.Verify( x => x.Delete( It.IsAny<CloudQueueMessage>() ), Times.Exactly( 1 ) );
        }

        [Test]
        public void TestProcessValidMessage()
        {
            mockQueue.Setup( x => x.Get( It.IsAny<TimeSpan>() ) ).Returns(
                new CloudQueueMessage( "{\"SomeId\":6,\"SomeString\":\"Some Message\"}" ) );

            qProcessor.ProcessQueue();

            mockQueue.Verify( x => x.Delete( It.IsAny<CloudQueueMessage>() ), Times.Exactly( 1 ) );
        }

    }
}
