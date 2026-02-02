namespace Modern.App.Common.Queries;

public interface IQueryExecutor
{
    Task<TResult> Execute<TResult>(IQuery<TResult> query);
}