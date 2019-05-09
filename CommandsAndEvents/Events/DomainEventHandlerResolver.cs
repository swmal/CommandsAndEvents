using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommandsAndEvents.Events
{
    /// <summary>
    /// Will scan all referenced assemblies for implementations of
    /// <see cref="EventStreamProvider"/>s and <see cref="IDomainEventHandler"/>s.
    /// </summary>

    public class DomainEventHandlerResolver
    {
        public DomainEventHandlerResolver()
        {
            if (!_handlersRegistred)
            {
                RegisterEventStreams();
                RegisterHandlers();
                _handlersRegistred = true;
            }
                
        }

        private static bool _handlersRegistred = false;
        private static readonly List<EventStreamProvider> _eventStreams = new List<EventStreamProvider>();
        private static Dictionary<Type, List<IDomainEventHandler>> _handlers = new Dictionary<Type, List<IDomainEventHandler>>();

        private void RegisterEventStreams()
        {
            var eventStreamTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a != typeof(DomainEventHandlerResolver).Assembly)
                .SelectMany(s => s.GetExportedTypes())
                .Where(t => t.IsAssignableFrom(typeof(EventStreamProvider)) && !t.IsAbstract && t.IsClass);
            if(eventStreamTypes == null || eventStreamTypes.Count() == 0)
            {
                _eventStreams.Add(EventStreamProvider.ConsoleLogger);
            }
            foreach (var eventStreamType in eventStreamTypes)
            {
                var eventStream = (EventStreamProvider)Activator.CreateInstance(eventStreamType);
                _eventStreams.Add(eventStream);
            }
        }

        private void RegisterHandlers()
        {
            // scan the assemblies for IDomainHandler instances
            var handlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetExportedTypes())
                .Where(t => t.IsAssignableFrom(typeof(IDomainEventHandler)) && !t.IsAbstract && t.IsClass);
            // add the handlers
            foreach (var handlerType in handlers)
            {
                if (!_handlers.ContainsKey(handlerType))
                    _handlers[handlerType] = new List<IDomainEventHandler>();
                foreach(var eventStream in _eventStreams)
                {
                    var handlerInstance = (IDomainEventHandler)Activator.CreateInstance(handlerType, new object[] { eventStream });
                    _handlers[handlerType].Add(handlerInstance);
                }
                
            }
        }

        /// <summary>
        /// Resolves an event type to instances of eventhandlers
        /// </summary>
        /// <param name="eventType">The event type to resolve</param>
        /// <returns>An IEnumerable of <see cref="IDomainEventHandler"/></returns>
        public IEnumerable<IDomainEventHandler> ResolveHandler(Type eventType)
        {
            if (!_handlers.ContainsKey(eventType))
            {
                throw new InvalidOperationException($"Type {eventType.Name} is not an instance of IDomainEventHandler.");
            }
            return _handlers[eventType];
        }
    }
}
