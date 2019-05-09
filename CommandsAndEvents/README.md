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

```csharp
 // An event
 public class ValueApplied : Event
 {
	public string Value { get; set;}
 }
  ```