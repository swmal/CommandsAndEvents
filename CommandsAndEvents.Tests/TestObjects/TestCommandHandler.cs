using CommandsAndEvents.Commands;
using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Tests.TestObjects
{
    public class TestCommandHandler : CommandHandler<TestAggregate, TestCommand>
    {
        public TestAggregate AggregateRoot { get; set; }

        protected override void ExecuteCommand(TestAggregate aggregateRoot, TestCommand command)
        {
            aggregateRoot.SetTest(command.Value);
        }

        protected override void OnCommandExecuted(TestAggregate aggregateRoot)
        {
            this.AggregateRoot = aggregateRoot;
        }
    }
}
