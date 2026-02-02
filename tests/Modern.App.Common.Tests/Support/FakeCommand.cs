using Modern.App.Common.Commands;

namespace Modern.App.Common.Tests.Support;

public record FakeCommand : ICommand;

public class FakeCommandHandler : ICommandHandler<FakeCommand>
{
    public Task Handle(FakeCommand request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}