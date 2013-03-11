using System;

namespace PollingWorkerRole.Helpers
{
    public class Payload
    {
        public long SomeId { get; set; }
        public string SomeString { get; set; }

        public override string ToString()
        {
            return String.Format( "{0}, {1}", SomeId, SomeString );
        }
    }
}
