namespace SharedKernal;

// Why abstract because, i dont want anyone to create an instance of Entity
public abstract class Entity : IEquatable<Entity>
{
    // once the Id is generated, no one should change it
    private Guid Id { get; init; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public bool Equals(Entity? other)
    {
        if (other is null) return false;

        if (other.GetType() != GetType())
        {
            return false;
        }

        if (other is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity first, Entity second)
    {
        return first.Equals(second);
    }
    
    public static bool operator !=(Entity first, Entity second)
    {
        return !(first == second);
    }
}