using Modern.App.Common.Commands;
using Modern.App.Common.Events;
using Modern.App.Common.Queries;

namespace Modern.App.Common;

public interface IMessenger : ICommandExecutor, IQueryExecutor, IEventPublisher;

public class Messenger(
    ICommandExecutor commandExecutor,
    IQueryExecutor queryExecutor,
    IEventPublisher eventPublisher
) : IMessenger
{
    public async Task Execute(ICommand command)
    {
        await commandExecutor.Execute(command).ConfigureAwait(false);
    }

    public async Task<TResult> Execute<TResult>(ICommand<TResult> command)
    {
        return await commandExecutor.Execute(command).ConfigureAwait(false);
    }

    public async Task<TResult> Execute<TResult>(IQuery<TResult> query)
    {
        return await queryExecutor.Execute(query).ConfigureAwait(false);
    }

    public async Task Publish(IEvent @event)
    {
        await eventPublisher.Publish(@event).ConfigureAwait(false);
    }
}