
using CommandsAndEvents.EventHandlers;
using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents
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
