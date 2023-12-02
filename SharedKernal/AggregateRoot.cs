namespace SharedKernal;

public abstract class AggregateRoot : Entity
{
    public AggregateRootState State { get; }
    protected AggregateRoot(Guid id) : base(id)
    {
    }
    protected void Apply(IDomainEvent @event)
    {
        State.Apply(@event);
    }
}