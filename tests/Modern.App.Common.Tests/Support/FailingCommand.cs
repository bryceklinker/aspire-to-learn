using Modern.App.Common.Commands;

namespace Modern.App.Common.Tests.Support;

public record FailingCommand : ICommand;

public class FailingCommandHandler : ICommandHandler<FailingCommand>
{
    public Task Handle(FailingCommand request, CancellationToken cancellationToken)
    {
        throw new Exception("This fails");
    }
}