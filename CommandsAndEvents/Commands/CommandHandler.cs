using CommandsAndEvents.Commands;
using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommandsAndEvents.Commands
{
    public abstract class CommandHandler<T, T1>
        where T : AggregateRoot, new()
        where T1 : Command<T>
    {
        public CommandHandler(IDomainEventHandlerResolver eventHandlerResolver)
        {
            _eventHandlerResolver = eventHandlerResolver 
                ?? throw new ArgumentNullException("eventHandlerResolver");
        }

        private readonly IDomainEventHandlerResolver _eventHandlerResolver;

        public CommandHandler(T1 command, T aggregateRoot)
        {
            _root = aggregateRoot;
            _command = command;
        }

        private readonly T _root;
        private readonly T1 _command;
        protected abstract void ExecuteCommand(T aggregateRoot, T1 command);

        protected virtual void OnCommandExecuted(T aggregateRoot) { }

        private void ValidateCommand()
        {
            var ctx = new ValidationContext(_command);
            Validator.ValidateObject(_command, ctx);
        }

        /// <summary>
        /// Execute the commandhandler on the supplied aggregate root.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <param name="command"></param>
        public void Execute(T aggregateRoot, T1 command)
        {
            ValidateCommand();
            ExecuteCommand(aggregateRoot, _command);
            foreach(var evt in _root.DomainEvents)
            {
                var handler = _eventHandlerResolver.ResolveHandler(evt.GetType());
                handler.HandleEvent(evt);
            }
            OnCommandExecuted(_root);
        }

        /// <summary>
        /// Execute the commandhandler, a new aggregate root will be instanciated.
        /// </summary>
        /// <param name="command"></param>
        public void Execute(T1 command)
        {
            Execute(new T(), command);
        }
    }
}
