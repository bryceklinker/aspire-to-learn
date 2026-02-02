using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Modern.App.Common.Queries;

namespace Modern.App.Common.Logging;

internal class LoggingBehavior<TRequest, TResult>(
    ILogger logger
) : IPipelineBehavior<TRequest, TResult> where TRequest : notnull
{
    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        var requestKind = typeof(TRequest).GetInterface(typeof(IQuery<TResult>).Name) == null
            ? "Command"
            : "Query";
        var stopwatch = Stopwatch.StartNew();
        using var scope = logger.BeginScope(new { });
        logger.LogInformation("Starting {RequestKind}...", requestKind);
        Exception? exception = null;
        try
        {
            return await next(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            exception = e;
            throw;
        }
        finally
        {
            stopwatch.Stop();
            if (exception != null)
            {
                logger.LogError("{RequestKind} failed after {ElapsedTime} ms with {@Exception}", requestKind, stopwatch.ElapsedMilliseconds, exception);
            }
            else
            {
                logger.LogInformation("{RequestKind} finished after {ElapsedTime} ms", requestKind, stopwatch.ElapsedMilliseconds);
            }    
        }
    }
}