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
            var assemblies = GetAssemblies();
            var allTypes = assemblies.SelectMany(s => s.GetExportedTypes());
                
            var eventStreamTypes = allTypes.Where(t => t.IsSubclassOf(typeof(EventStreamProvider)) && !t.IsAbstract && t.IsClass);
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
            var handlers = GetAssemblies()
                .SelectMany(s => s.GetExportedTypes())
                .Where(t => typeof(IDomainEventHandler).IsAssignableFrom(t) && !t.IsAbstract && t.IsClass);
            // add the handlers
            foreach (var handlerType in handlers)
            {
                var baseType = handlerType.BaseType;
                var eventType = baseType.GetGenericArguments()[0];
                if (!_handlers.ContainsKey(eventType))
                    _handlers[eventType] = new List<IDomainEventHandler>();
                foreach(var eventStream in _eventStreams)
                {
                    var handlerInstance = (IDomainEventHandler)Activator.CreateInstance(handlerType, new object[] { eventStream });
                    _handlers[eventType].Add(handlerInstance);
                }
                
            }
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a != typeof(DomainEventHandlerResolver).Assembly 
                && !a.IsDynamic 
                && !a.FullName.StartsWith("System") 
                && !a.FullName.StartsWith("Microsoft") 
                && !a.FullName.StartsWith("Newtonsoft")
                && !a.FullName.StartsWith("Eventstore"));
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
