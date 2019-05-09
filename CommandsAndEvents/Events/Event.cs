using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    /// <summary>
    /// Base class for domain events
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Id of the event
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Timestamp for when the event was created.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Name of the stream on which the event should be published.
        /// </summary>
        [JsonIgnore]
        public abstract string Stream { get; }
    }
}
