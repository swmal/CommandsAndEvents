using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Tests.TestObjects
{
    public class TestEventHandler : DomainEventHandler<TestEvent>
    {
        public TestEventHandler(EventStreamProvider eventStream) : base(eventStream)
        {
        }
    }
}
