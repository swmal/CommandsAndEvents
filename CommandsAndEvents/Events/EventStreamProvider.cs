using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    /// <summary>
    /// Base class for classes implementing a publishing mechanism 
    /// for domain events
    /// </summary>
    public abstract class EventStreamProvider
    {
        /// <summary>
        /// Publishes a domain event on a stream
        /// </summary>
        /// <param name="eventId">Id of the event</param>
        /// <param name="stream">Name of the stream</param>
        /// <param name="eventName">event type</param>
        /// <param name="data">byte array containing the event data</param>
        public abstract void Publish(Guid eventId, string stream, string eventName, byte[] data);

        public static EventStreamProvider Default = new DefaultEventstreamProvider();
    }
}
