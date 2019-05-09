using CommandsAndEvents.Commands;
using CommandsAndEvents.Events;
using CommandsAndEvents.Tests.TestObjects;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    }
}
