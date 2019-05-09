using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreDemo.Domain.EventStore
{
    public static class EventStoreStreamConnection
    {
        private static IEventStoreConnection _connection = null;
        private readonly static object _syncRoot = new object();

        private static string GetEventStoreUrl()
        {
            var envVar = Environment.GetEnvironmentVariable("COMMANDS_EVENTS_EVENTSTORE_URL");
            if (string.IsNullOrEmpty(envVar))
                return "tcp://admin:changeit@localhost:1113";
            return envVar;
        }

        public static IEventStoreConnection GetConnection()
        {
            lock (_syncRoot)
            {
                if (_connection == null)
                {
                    _connection = EventStoreConnection.Create(new Uri(GetEventStoreUrl()), "DomainEventHandler");
                    _connection.ConnectAsync().Wait();
                }
            }
            return _connection;
        }
    }
}
