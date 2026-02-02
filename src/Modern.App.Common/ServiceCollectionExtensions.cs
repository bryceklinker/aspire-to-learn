using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Modern.App.Common.Commands;
using Modern.App.Common.Events;
using Modern.App.Common.Logging;
using Modern.App.Common.Queries;

namespace Modern.App.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModernAppCommon(
        this IServiceCollection services,
        params Assembly[] assembliesToScan)
    {

        return services.AddModernAppCommon(null, assembliesToScan);
    }

    public static IServiceCollection AddModernAppCommon(
        this IServiceCollection services,
        Action<ILoggingBuilder>? configureLogging = null,
        params Assembly[] assembliesToScan
    )
    {
        services.AddLogging(log => { configureLogging?.Invoke(log); });
        services.AddMediatR(m =>
        {
            m.RegisterServicesFromAssemblies(assembliesToScan);
            m.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddValidatorsFromAssemblies(assembliesToScan);
        services.TryAddTransient<IQueryExecutor, QueryExecutor>();
        services.TryAddTransient<ICommandExecutor, CommandExecutor>();
        services.TryAddTransient<IEventPublisher, EventPublisher>();
        services.TryAddTransient<IMessenger, Messenger>();
        return services;
    }
}