using Modern.App.Common.Queries;

namespace Modern.App.Common.Tests.Support;

public record FakeQuery : IQuery<Guid>;

public class FakeQueryHandler : IQueryHandler<FakeQuery, Guid>
{
    public Task<Guid> Handle(FakeQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}