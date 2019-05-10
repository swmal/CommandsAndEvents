using CommandsAndEvents.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CommandsAndEvents.Events
{
    public class DomainEventHandler<T> : IDomainEventHandler
        where T : Event
    {
        public DomainEventHandler(EventStreamProvider eventStream)
        {
            _eventStream = eventStream;
        }

        private readonly EventStreamProvider _eventStream;
        private T CastEvent(Event e)
        {
            return e as T;
        }

        private byte[] EventToJsonBytes(T e)
        {
            var s = JsonConvert.SerializeObject(e);
            return Encoding.UTF8.GetBytes(s);
        }

        private void ValidateEvent(T evt)
        {
            var ctx = new ValidationContext(evt);
            Validator.ValidateObject(evt, ctx);
        }
        
        public virtual void HandleEvent(Event evt)
        {
            var e = CastEvent(evt);
            ValidateEvent(e);
            var data = EventToJsonBytes(e);
            _eventStream.Publish(evt.Id, evt.Stream, typeof(T).Name, data);
        }
    }
}
