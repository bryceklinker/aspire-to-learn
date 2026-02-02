using MediatR;

namespace Modern.App.Common.Queries;

internal class QueryExecutor(IMediator mediator) : IQueryExecutor
{
    public async Task<TResult> Execute<TResult>(IQuery<TResult> query)
    {
        return await mediator.Send(query).ConfigureAwait(false);
    }
}