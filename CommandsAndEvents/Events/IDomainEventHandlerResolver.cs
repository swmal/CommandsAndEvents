using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    public interface IDomainEventHandlerResolver
    {
        /// <summary>
        /// Interface for domain event resolvers
        /// </summary>
        /// <param name="type">The event type, to be resolved to an event handler</param>
        /// <returns>A <see cref="IDomainEventHandler"/></returns>
        IDomainEventHandler ResolveHandler(Type type);
    }
}
