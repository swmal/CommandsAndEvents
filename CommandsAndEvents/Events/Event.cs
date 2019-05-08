using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    public abstract class Event
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public abstract string Stream { get; }
    }
}
