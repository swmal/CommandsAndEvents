using System;
using System.Collections.Generic;
using System.Text;
using CommandsAndEvents.Events;

namespace CommandsAndEvents.Events
{
    public interface IDomainEventHandler
    {
        void HandleEvent(Event e);
    }
}
