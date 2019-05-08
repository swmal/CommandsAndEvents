using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents
{
    public abstract class AggregateRoot
    {
        private List<Event> _events = new List<Event>();
        public Guid Id { get; set; }

        public IEnumerable<Event> DomainEvents => _events;

        public void AddEvent(Event e)
        {
            e.Id = Guid.NewGuid();
            e.Timestamp = DateTime.Now;
            _events.Add(e);
        }
    }
}
