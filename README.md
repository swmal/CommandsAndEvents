# CommandsAndEvents
Lightweight library with support for commands, domain events and aggregates.

Let´s have a look at the basic concepts.

## Aggregates
 ```csharp
 // An aggregate root
 public class MyAggregateRoot : AggregateRoot
 {
	public string Value{get; set;}
	
	public void ApplyValue(string value)
	{
		this.Value = value;
		// emit a domain event
		EmitEvent(new ValueApplied{ Value = value });
	}
 }

  ```
  ## Commands
  Commands can be decorated with DataAnnotations and will be validated
  by this library when executed.
   ```csharp
 // A command
 public class ApplyValue : Command<MyAggregateRoot>
 {
	[Required]
	public string Value { get; set;}
 }

  ```
 ## Events
```csharp
 // An event
 public class ValueApplied : Event
 {
	public string Value { get; set;}

	public override string Stream => "MyAggregateRoot";
 }
  ```
  The thought behind this library is that we have one eventstream per aggregate.
  To avoid having to specify the stream on each event, we can create a base class
  for each aggregate and seal the Stream property.

```csharp
 public abstract class MyAggregateRootEvent : Event
 {

	public override sealed string Stream => "MyAggregateRoot";
 }

 public class ValueApplied : MyAggregateRootEvent
 {
	public string Value { get; set;}
 }
  ```

  ## Event handlers
  Event handlers will register automatically and append the event to
  the stream specified in the Event.
  To publish the event the Event handler will use any class that inherits
  the EventStreamProvider class available in this library.
  ```csharp
  // an eventhandler for the event above
  public class ValueAppliedEventHandler : DomainEventHandler<ValueApplied>
  {

  }

  ```
  ## Command handlers
  ```csharp
  public class ApplyValueCommandHandler : CommandHandler<MyAggregateRoot, ApplyValue>
  {
	  protected override void ExecuteCommand(MyAggregateRoot aggregateRoot, ApplyValue command)
	  {
	  	  aggregateRoot.ApplyValue(command.Value);
	  }
  }
  ```