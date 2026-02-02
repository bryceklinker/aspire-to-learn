using Microsoft.Extensions.DependencyInjection;

namespace Modern.App.Test.Support;

public static class ServiceProviderFactory
{
    public static IServiceProvider Create(Action<IServiceCollection> configure)
    {
        var services = new ServiceCollection();
        configure(services);
        services.AddModernAppFakeServices();
        return services.BuildServiceProvider();
    }
}