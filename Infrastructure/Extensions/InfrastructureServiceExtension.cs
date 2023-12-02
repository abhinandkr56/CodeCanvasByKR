using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddSingleton<EventStoreImplementation>();
        services.AddSingleton<IEventStoreRepository, EventStoreRepository>();

        return services;
    }
}