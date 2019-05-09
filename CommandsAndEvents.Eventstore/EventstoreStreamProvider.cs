/*using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using EventStoreDemo.Domain.Events;

namespace EventStoreDemo.Domain.EventStore
{
    public class EventstoreStreamProvider : EventStreamProvider
    {
        public override void Publish(Guid eventId, string stream, string eventName, byte[] data)
        {
            var connection = EventStoreStreamConnection.GetConnection();
            var eventData = new EventData(eventId, eventName, true, data, null);
            connection.AppendToStreamAsync(stream, ExpectedVersion.Any, eventData).Wait();
        }
    }
}
*/