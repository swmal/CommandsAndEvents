using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents
{
    /// <summary>
    /// Base class for aggregate roots.
    /// </summary>
    public abstract class AggregateRoot
    {
        public AggregateRoot()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _events = new List<Event>();

        /// <summary>
        /// Id of the Aggregate root
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Domain events emitted after a command is executed.
        /// </summary>
        public IEnumerable<Event> DomainEvents => _events;

        /// <summary>
        /// Inheriting classes should use this method to
        /// emit domain events
        /// </summary>
        /// <param name="e">The <see cref="Event"/> to emit.</param>
        protected void EmitEvent(Event e)
        {
            e.Id = Guid.NewGuid();
            e.Timestamp = DateTime.Now;
            _events.Add(e);
        }
    }
}
