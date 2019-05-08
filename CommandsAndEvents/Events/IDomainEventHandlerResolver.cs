using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    public interface IDomainEventHandlerResolver
    {
        IDomainEventHandler ResolveHandler(Type type);
    }
}
