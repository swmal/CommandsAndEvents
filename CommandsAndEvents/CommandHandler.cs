using CommandsAndEvents.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommandsAndEvents
{
    public abstract class CommandHandler<T, T1>
        where T : AggregateRoot, new()
        where T1 : Command<T>
    {
        public CommandHandler(T1 command)
        {
            _root = new T();
            _command = command;
        }

        public abstract IDomainEventHandlerResolver EventHandlerResolver { get; }

        protected T1 Handler { get; private set; }

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

        public void Execute()
        {
            ValidateCommand();
            ExecuteCommand(_root, _command);
            foreach(var evt in _root.DomainEvents)
            {
                var handler = EventHandlerResolver.ResolveHandler(evt.GetType());
                handler.HandleEvent(evt);
            }
            OnCommandExecuted(_root);
        }
    }
}
