using Modern.App.Common.Queries;

namespace Modern.App.Common.Tests.Support;

public record FailingQuery : IQuery<string>;

public class FailingQueryHandler : IQueryHandler<FailingQuery, string>
{
    public Task<string> Handle(FailingQuery request, CancellationToken cancellationToken)
    {
        throw new Exception("failing query");
    }
}