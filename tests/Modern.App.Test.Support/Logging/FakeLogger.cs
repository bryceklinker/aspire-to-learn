using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Modern.App.Test.Support.Logging;

public record FakeLogItem(LogLevel Level, EventId EventId, object? State, Exception? Exception, string Message);

public class FakeLogger : ILogger, IDisposable
{
    private readonly ConcurrentBag<FakeLogItem> _logItems = [];

    public FakeLogItem[] GetLogItems(LogLevel level)
    {
        return _logItems.Where(l => l.Level == level).ToArray();
    }
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _logItems.Add(new FakeLogItem(logLevel, eventId, state, exception, formatter(state, exception)));
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    public void Dispose()
    {
        
    }
}