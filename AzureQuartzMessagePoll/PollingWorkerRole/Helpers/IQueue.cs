using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingWorkerRole.Models
{
    public interface IQueue<TMessageType>
    {
        string Name { get; }
        TMessageType Get( TimeSpan visibilityTimeout );
        void Put( TMessageType queuePayload );
        void Delete( TMessageType queuePayload );
    }
}
