using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Tests.TestObjects
{
    public class TestEvent : Event
    {
        public override string Stream => throw new NotImplementedException();
    }
}
