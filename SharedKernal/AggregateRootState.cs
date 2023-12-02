namespace SharedKernal;

public class AggregateRootState 
{
    public AggregateRootState()
    {
        
    }
    public void Apply(IDomainEvent @event)
    {
        dynamic target = this;
        target.When(@event);
    }
}
