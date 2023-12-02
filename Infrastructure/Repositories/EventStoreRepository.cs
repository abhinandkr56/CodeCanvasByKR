using Application.Interfaces;
using SharedKernal;

namespace Infrastructure.Repositories;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly EventStoreImplementation _eventStore;

    public EventStoreRepository(EventStoreImplementation eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task SaveEventAsync(string streamName, IDomainEvent domainEvent)
    {
        await _eventStore.AppendEvents(streamName, domainEvent);
    }

    public async Task<T> GetEvents<T>(string streamName) where T : AggregateRootState, new()
    {
        return await _eventStore.LoadAsync<T>(streamName);
    }

    public string GetEventStreamName(Type type, Guid id)
    {
        return _eventStore.GetAggregateStreamName(type, id);
    }
}