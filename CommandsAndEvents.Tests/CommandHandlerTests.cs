using CommandsAndEvents.Commands;
using CommandsAndEvents.Events;
using CommandsAndEvents.Tests.TestObjects;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandsAndEvents.Tests
{
    [TestClass]
    public class CommandHandlerTests
    {
        [TestMethod]
        public void ShouldCallOnCommandExecuted()
        {
            var command = new TestCommand
            {
                Value = "Hello"
            };
            var commandHandler = new TestCommandHandler();
            commandHandler.Execute(command);
            Assert.AreEqual("Hello", commandHandler.AggregateRoot.Value);
        }

        [TestMethod]
        public void ShouldEmitEventsToStream()
        {
            var streams = new InMemoryEventStreams();
            var handler = new TestCommandHandler();
            var command = new TestCommand { Value = "test" };
            handler.Execute(command);
            var stream = streams.GetStream("MyTestStream");
            Assert.AreEqual(1, stream.Count, "stream.Count was not 1");
            Assert.AreEqual("TestEvent", stream.First().EventName);

        }
    }

}
