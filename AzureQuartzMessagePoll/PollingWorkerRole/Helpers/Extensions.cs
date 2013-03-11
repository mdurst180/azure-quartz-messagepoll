using Microsoft.WindowsAzure.StorageClient;
using Newtonsoft.Json;

namespace PollingWorkerRole.Helpers
{
    public static class Extensions
    {
        public static TMessagePayloadType GetMessagePayload<TMessagePayloadType>( this CloudQueueMessage message )
        {
            return JsonConvert.DeserializeObject<TMessagePayloadType>( message.AsString );
        }

        /// <summary>
        /// Serializes this object as JSON.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string ToJson( this object obj )
        {
            return JsonConvert.SerializeObject( obj );
        }
    }
}
