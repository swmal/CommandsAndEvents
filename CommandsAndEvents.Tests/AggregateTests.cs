using CommandsAndEvents.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CommandsAndEvents.Tests
{
    [TestClass]
    public class AggregateTests
    {
        [TestMethod]
        public void IdShouldBeSet()
        {
            var aggregate = new TestAggregate();
            Assert.IsNotNull(aggregate.Id, "aggregate.Id was null");
            Assert.AreNotEqual(Guid.Empty, aggregate.Id, "aggregate.Id was Guid.Empty");
        }

        [TestMethod]
        public void EventShouldBeEmitted()
        {
            var aggregate = new TestAggregate();
            aggregate.EmitTestEvent();
            Assert.AreEqual(1, aggregate.DomainEvents.Count(), "no events were present in aggregate.DomainEvents");
            Assert.IsInstanceOfType(aggregate.DomainEvents.First(), typeof(TestEvent), "Event was not of expected type");
        }
    }
}
