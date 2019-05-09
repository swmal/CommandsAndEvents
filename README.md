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
   ```csharp
 // A command
 public class ApplyValue : Command<MyAggregateRoot>
 {
	public string Value { get; set;}
 }

  ```
 ## Events
```csharp
 // An event
 public class ValueApplied : Event
 {
	public string Value { get; set;}

	public override string Stream => "MyTestStream";
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
  public class ApplyValueCommandHandler<MyAggregateRoot, ApplyValue>
  {
	  protected override void ExecuteCommand(MyAggregateRoot aggregateRoot, ApplyValue command)
	  {
	  	  aggregateRoot.ApplyValue(command.Value);
	  }
  }
  ```