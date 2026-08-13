using Microsoft.Extensions.DependencyInjection;

namespace Kavsar.Modules.Organization;

public static class OrganizationModuleServiceCollectionExtensions
{
    public static IServiceCollection AddOrganizationModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services;
    }
}
