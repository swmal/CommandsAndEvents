using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    public class DomainEventHandlers
    {
        public static void Publish(Event e, DomainEventHandlerResolver resolver)
        {
            var type = e.GetType();
            var handlers = resolver.ResolveHandler(type);
            foreach(var handler in handlers)
                handler.HandleEvent(e);
        }
    }
}
