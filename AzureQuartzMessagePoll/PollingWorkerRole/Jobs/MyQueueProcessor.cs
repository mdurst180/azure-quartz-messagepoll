using System;
using Autofac;
using PollingWorkerRole.Helpers;

namespace PollingWorkerRole.Jobs
{
    public class MyQueueProcessor : BaseQueueProcessor<Payload>
    {
        public MyQueueProcessor( IContainer container )
            : base( container )
		{
            //this.enrollmentService = container.Resolve<IEnrollmentService>();
		}
        /// <summary>
        /// Processes the given message
        /// </summary>
        /// <param name="message">A message from the Cloud Queue</param>
        /// <returns>
        ///   <b>true</b> if the message is to be dequeued;
        /// otherwise returns <b>false.</b>
        /// </returns>
        protected override bool ProcessMessage( Payload message )
        {
            Console.WriteLine( String.Format( "Got Message {0}: {1}", message.SomeId, message.SomeString ) );
            return true;
        }
    }
}
