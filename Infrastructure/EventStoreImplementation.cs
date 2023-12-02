using System.Text;
using EventStore.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SharedKernal;

namespace Infrastructure;

public class EventStoreImplementation
{
    private readonly IConfiguration _configuration;
    private readonly EventStoreClient _client;

    public EventStoreImplementation(IConfiguration configuration)
    {
        _configuration = configuration;
        var settings = EventStoreClientSettings.Create(_configuration["eventStoreGrpc:connectionString"]);
        _client = new EventStoreClient(settings);
    }
    
    public async Task AppendEvents(string streamName, object data)
    {
        var eventData = new EventData(
            Uuid.NewUuid(), 
            data.GetType().Name, 
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)), 
            metadata: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(DateTime.UtcNow)));

        await _client.AppendToStreamAsync(
            streamName, 
            StreamState.Any, 
            new[] { eventData });
    }

    public async Task<T> LoadAsync<T>(string streamName) where T : AggregateRootState, new()
    {
        var aggregate = new T();
        var events = _client.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);

        await foreach (var resolvedEvent in events)
        {
            var eventType = Type.GetType(resolvedEvent.Event.EventType);
            var eventData = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span), eventType);

            aggregate.Apply(eventData as IDomainEvent);
        }

        return aggregate;
    }

    public string GetAggregateStreamName(Type aggregateRootType, Guid aggregateId)
    {
        string streamName;

        streamName = $"{aggregateRootType.Name}-{aggregateId}";
        
        return streamName;
    }
    
}