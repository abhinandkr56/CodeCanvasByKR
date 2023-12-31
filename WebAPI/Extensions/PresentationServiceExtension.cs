using Application.Behaviors;
using Application.BlogPost.Commands;
using Infrastructure.Extensions;
using Serilog;
using Endpoint = NServiceBus.Endpoint;

namespace WebAPI.Extensions;

public static class PresentationServiceExtension
{
    public static async Task<IServiceCollection> AddPresentationDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        var endpointConfiguration = new EndpointConfiguration("CodeCanvasByKR");

        var scanner = endpointConfiguration.AssemblyScanner();
        scanner.ScanAssembliesInNestedDirectories = true;
        scanner.ThrowExceptions = true;

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

        //docker pull rabbitmq:3-management
        //docker run -d --hostname my-rabbit --name some-rabbit -p 8080:15672 -p 5672:5672 rabbitmq:3-management
        transport.ConnectionString(configuration["RabbitMq:connectionString"]);
        var routing = transport.Routing();
        routing.RouteToEndpoint(assembly: typeof(CreateBlogPostCommand).Assembly,
            destination: "CodeCanvasByKR");
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.Pipeline.Register(behavior: new ErrorHandlerBehavior(), description: "Handles the error");
        endpointConfiguration.RegisterComponents(
            registration: configureComponents =>
            {
                configureComponents.AddLogging(c => c.AddSerilog(Log.Logger));
                configureComponents.AddSingleton(configuration);
                configureComponents.AddInfrastructureDependencies();
            });
        IEndpointInstance endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        services.AddSingleton(endpointInstance);
        return services;
    }
}