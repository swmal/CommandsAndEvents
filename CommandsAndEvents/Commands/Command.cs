using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Commands
{
    public class Command<T>
        where T : AggregateRoot, new()
    {
    }
}
