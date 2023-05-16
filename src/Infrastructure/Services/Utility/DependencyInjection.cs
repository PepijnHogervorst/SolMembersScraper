using Application.Services;
using Domain.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Utility;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddUtility();
        services.AddXmlServices();
        return services;
    }

    private static IServiceCollection AddUtility(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddXmlServices(this IServiceCollection services)
    {
        services.AddSingleton<IXmlFileFinder, XmlFileFinder>();
        services.AddSingleton<ISolMembersXmlReader, SolMembersXmlReader>();

        return services;
    }
}