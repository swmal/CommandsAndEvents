using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Tests.TestObjects
{
    public class InMemoryEventStreams : EventStreamProvider
    {
        public class StreamEvent
        {
            public Guid Id { get; set; }
            public string EventName { get; set; }

            public string Data { get; set; }
        }

        private static Dictionary<string, List<StreamEvent>> _streams = new Dictionary<string, List<StreamEvent>>();

        public List<StreamEvent> GetStream(string name)
        {
            return _streams[name];
        }

        public override void Publish(Guid eventId, string stream, string eventName, byte[] data)
        {
            if (!_streams.ContainsKey(stream))
                _streams[stream] = new List<StreamEvent>();
            _streams[stream].Add(new StreamEvent
            {
                EventName = eventName,
                Id = eventId,
                Data = Encoding.UTF8.GetString(data)
            });
        }
    }
}
