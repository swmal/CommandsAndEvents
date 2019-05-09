using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Tests.TestObjects
{
    public class TestAggregate : AggregateRoot
    {
        public string Value { get; set; }

        public void EmitTestEvent()
        {
            EmitEvent(new TestEvent());
        }

        public void SetTest(string value)
        {
            Value = value;
        }
    }
}
