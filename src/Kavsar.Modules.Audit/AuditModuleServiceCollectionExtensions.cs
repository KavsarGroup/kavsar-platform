using Microsoft.Extensions.DependencyInjection;

namespace Kavsar.Modules.Audit;

public static class AuditModuleServiceCollectionExtensions
{
    public static IServiceCollection AddAuditModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services;
    }
}
