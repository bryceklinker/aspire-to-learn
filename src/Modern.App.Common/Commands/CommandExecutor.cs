using MediatR;

namespace Modern.App.Common.Commands;

internal class CommandExecutor(IMediator mediator) : ICommandExecutor
{
    public async Task Execute(ICommand command)
    {
        await mediator.Send(command).ConfigureAwait(false);
    }

    public async Task<TResult> Execute<TResult>(ICommand<TResult> command)
    {
        return await mediator.Send(command).ConfigureAwait(false);
    }
}