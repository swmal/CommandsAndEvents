using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Tests.TestObjects
{
    public class TestEvent : Event
    {
        public string Value { get; set; }
        public override string Stream => "MyTestStream";
    }
}
