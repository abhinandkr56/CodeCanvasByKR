using Endpoint = NServiceBus.Endpoint;

namespace WebAPI.Extensions;

public static class PresentationServiceExtension
{
    public static async Task<IServiceCollection> AddPresentationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var endpointConfiguration = new EndpointConfiguration("CodeCanvasByKR");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        
        //docker pull rabbitmq:3-management
        //docker run -d --hostname my-rabbit --name some-rabbit -p 8080:15672 -p 5672:5672 rabbitmq:3-management
        transport.ConnectionString("host=localhost;username=guest;password=guest");
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        
        endpointConfiguration.EnableInstallers();

        IEndpointInstance endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        services.AddSingleton(endpointInstance);
        return services;
    }
}