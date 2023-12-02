using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    {
        return services;
    }
}