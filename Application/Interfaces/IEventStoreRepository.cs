using SharedKernal;

namespace Application.Interfaces;

public interface IEventStoreRepository
{
    Task SaveEventAsync(string streamName, IDomainEvent domainEvent);

    Task<T> GetEvents<T>(string streamName) where T : AggregateRootState, new();

    string GetEventStreamName(Type type, Guid id);
}