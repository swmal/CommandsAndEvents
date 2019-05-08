using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    public class DomainEventHandlers
    {
        public static void Publish(Event e, IDomainEventHandlerResolver resolver)
        {
            var type = e.GetType();
            var handler = resolver.ResolveHandler(type);
            handler.HandleEvent(e);
        }
    }
}
