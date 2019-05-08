using CommandsAndEvents.EventHandlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents
{
    public interface IDomainEventHandlerResolver
    {
        IDomainEventHandler ResolveHandler(Type type);
    }
}
