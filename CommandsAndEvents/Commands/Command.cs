using System;
using System.Collections.Generic;
using System.Text;

namespace CommandsAndEvents.Commands
{
    /// <summary>
    /// Base class for commands.
    /// The properties of a command can be decorated with
    /// data validation attributes (available in the
    /// System.ComponentModel.DataAnnotations, a separate Nuget
    /// package in .NET core)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Command<T>
        where T : AggregateRoot, new()
    {
    }
}
