using CommandsAndEvents.Commands;
using CommandsAndEvents.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommandsAndEvents.Commands
{
    /// <summary>
    /// Base class for commandhandlers
    /// </summary>
    /// <typeparam name="T">The aggregate root type</typeparam>
    /// <typeparam name="T1">The command type</typeparam>
    public abstract class CommandHandler<T, T1>
        where T : AggregateRoot, new()
        where T1 : Command<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventHandlerResolver">A <see cref="IDomainEventHandlerResolver"/></param>
        public CommandHandler(IDomainEventHandlerResolver eventHandlerResolver)
        {
            _eventHandlerResolver = eventHandlerResolver 
                ?? throw new ArgumentNullException("eventHandlerResolver");
        }

        private readonly IDomainEventHandlerResolver _eventHandlerResolver;

        /// <summary>
        /// Executes a command on the aggregate root.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to which the command will be applied.</param>
        /// <param name="command">The command to execute.</param>
        protected abstract void ExecuteCommand(T aggregateRoot, T1 command);

        /// <summary>
        /// Override this method to run after the command has been executed.
        /// Can be used to commit transactions and/or save the aggregate root
        /// to a repository.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to which the command has been applied.</param>
        protected virtual void OnCommandExecuted(T aggregateRoot) { }

        /// <summary>
        /// Override this method to run code just before the command is executed.
        /// Can be used to start a transaction.
        /// </summary>
        protected virtual void OnExecuteCommand() { }

        private void ValidateCommand(T1 command)
        {
            var ctx = new ValidationContext(command);
            Validator.ValidateObject(command, ctx);
        }

        /// <summary>
        /// Execute the commandhandler on the supplied aggregate root.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root to which the command will be applied.</param>
        /// <param name="command">The command to execute.</param>
        public void Execute(T aggregateRoot, T1 command)
        {
            ValidateCommand(command);
            OnExecuteCommand();
            ExecuteCommand(aggregateRoot, command);
            foreach(var evt in aggregateRoot.DomainEvents)
            {
                var handler = _eventHandlerResolver.ResolveHandler(evt.GetType());
                handler.HandleEvent(evt);
            }
            OnCommandExecuted(aggregateRoot);
        }

        /// <summary>
        /// Execute the commandhandler, a new aggregate root will be instanciated.
        /// </summary>
        /// <param name="command">The command to execute</param>
        public void Execute(T1 command)
        {
            Execute(new T(), command);
        }
    }
}
