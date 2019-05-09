# CommandsAndEvents
Lightweight library with support for command, events and aggregates

## Aggregates
 ```csharp
 // An aggregate root
 public class MyAggregateRoot : AggregateRoot
 {
	public string Value{get; set;}
	
	public void ApplyValue(string value)
	{
		this.Value = value;
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
 }
  ```

  ## Event handlers
  ### Event handler
  ```csharp
  // an eventhandler for the event above
  public class ValueAppliedEventHandler : DomainEventHandler<ValueApplied>
  {
	  // Add a default constructor that injects a EventStreamProvider that logs to the console.
	  public ValueAppliedEventHandler() : this(EventStreamProvider.Default){}

	  // constructor injection of an EventStreamProvider
  	  public ValueAppliedEventHandler(EventStreamProvider eventStream) : base(eventStream){}
  }

    ```

	### Register Event handlers
	```csharp
	public class MyEventHandlerResolver : IDomainHandlerResolver
	{
		IDomainEventHandler ResolveHandler(Type type)
		{
			private static Dictionary<Type, IDomainEventHandler> _handlers => new Dictionary<Type, IDomainEventHandler>()
			{
				{ typeof(ValueApplied), new ValueAppliedEventHandler() }
			};
		}
	}
	```