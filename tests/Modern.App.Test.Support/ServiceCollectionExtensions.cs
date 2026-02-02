using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Modern.App.Test.Support.Logging;

namespace Modern.App.Test.Support;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModernAppFakeServices(this IServiceCollection services)
    {
        var logger = new FakeLogger();
        services.ReplaceWithInstance<ILogger, FakeLogger>(logger);
        return services;
    }

    public static IServiceCollection ReplaceWithInstance<TService, TImplementation>(this IServiceCollection services,
        TImplementation instance)
        where TService : class
        where TImplementation : class, TService
    {
        services.RemoveAll(typeof(TService));
        services.AddSingleton(instance);
        services.AddSingleton<TService>(instance);
        return services;
    }
}