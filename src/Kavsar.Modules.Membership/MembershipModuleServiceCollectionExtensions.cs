using Microsoft.Extensions.DependencyInjection;

namespace Kavsar.Modules.Membership;

public static class MembershipModuleServiceCollectionExtensions
{
    public static IServiceCollection AddMembershipModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services;
    }
}
