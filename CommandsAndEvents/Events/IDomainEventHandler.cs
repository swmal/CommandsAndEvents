using System;
using System.Collections.Generic;
using System.Text;
using CommandsAndEvents.Events;

namespace CommandsAndEvents.Events
{
    /// <summary>
    /// Interface for domain event handlers
    /// </summary>
    public interface IDomainEventHandler
    {
        /// <summary>
        /// Sets an <see cref="EventStreamProvider"/> on this handler.
        /// </summary>
        /// <param name="eventStream"></param>
        void SetEventStreamProvider(EventStreamProvider eventStream);
        /// <summary>
        /// Handles the event
        /// </summary>
        /// <param name="e">The <see cref="Event"/> to handle</param>
        void HandleEvent(Event e);
    }
}
