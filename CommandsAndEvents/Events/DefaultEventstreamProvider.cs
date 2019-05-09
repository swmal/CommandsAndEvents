using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Events
{
    public class DefaultEventstreamProvider : EventStreamProvider
    {
        public override void Publish(Guid eventId, string stream, string eventName, byte[] data)
        {
            Console.WriteLine("==== Event published ====");
            Console.WriteLine($"Id: {eventId.ToString()}");
            Console.WriteLine($"Stream: {stream}");
            Console.WriteLine($"Eventname: {eventName}");
            var dataStr = Encoding.UTF8.GetString(data);
            Console.WriteLine(dataStr);
        }
    }
}
