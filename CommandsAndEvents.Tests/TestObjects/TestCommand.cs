using CommandsAndEvents.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Tests.TestObjects
{
    public class TestCommand : Command<TestAggregate>
    {
        public string Value { get; set; }
    }
}
