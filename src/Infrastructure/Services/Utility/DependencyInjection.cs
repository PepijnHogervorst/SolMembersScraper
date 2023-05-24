using Application.Services;
using Application.Stores;
using Domain.Common.Interfaces;
using Infrastructure.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Utility;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddUtility();
        services.AddXmlServices();
        services.AddServices();
        services.AddStores();
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

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ISolMembersBeautifier, SolMembersBeautifier>();

        return services;
    }

    private static IServiceCollection AddStores(this IServiceCollection services)
    {
        services.AddSingleton<IAreaCodesStore, AreaCodesNetherlands>();

        return services;
    }
}