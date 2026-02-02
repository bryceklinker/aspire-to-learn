using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Modern.App.Common.Tests.Support;
using Modern.App.Test.Support;
using Modern.App.Test.Support.Logging;

namespace Modern.App.Common.Tests.Logging;

public class LoggingBehaviorTests
{
    private readonly FakeLogger _logger;
    private readonly IMessenger _messenger;

    public LoggingBehaviorTests()
    {
        var provider = ServiceProviderFactory.Create(s =>
        {
            s.AddModernAppCommon(
                typeof(ServiceCollectionExtensions).Assembly,
                typeof(LoggingBehaviorTests).Assembly
            );
        });
        
        _logger = provider.GetRequiredService<FakeLogger>();
        _messenger = provider.GetRequiredService<IMessenger>();
    }

    [Fact]
    public async Task WhenCommandIsExecutedThenLogsStartOfCommand()
    {
        await _messenger.Execute(new FakeCommand());

        var logs = _logger.GetLogItems(LogLevel.Information);
        Assert.Equal(2, logs.Length);
        Assert.Contains(logs, l => l.Message.Contains("Starting Command"));
    }

    [Fact]
    public async Task WhenCommandIsExecutedThenEndOfCommandIsLogged()
    {
        await _messenger.Execute(new FakeCommand());
        
        var logs = _logger.GetLogItems(LogLevel.Information);
        Assert.Contains(logs, l => l.Message.Contains("Command finished"));
    }

    [Fact]
    public async Task WhenCommandFailsThenFailureOfCommandIsLogged()
    {
        await Assert.ThrowsAsync<Exception>(() => _messenger.Execute(new FailingCommand()));
        
        var logs = _logger.GetLogItems(LogLevel.Error);
        Assert.Contains(logs, l => l.Message.Contains("Command failed"));
    }
    
    [Fact]
    public async Task WhenQueryIsExecutedThenLogsStartOfQuery()
    {
        await _messenger.Execute(new FakeQuery());

        var logs = _logger.GetLogItems(LogLevel.Information);
        Assert.Equal(2, logs.Length);
        Assert.Contains(logs, l => l.Message.Contains("Starting Query"));
    }

    [Fact]
    public async Task WhenQueryIsExecutedThenEndOfQueryIsLogged()
    {
        await _messenger.Execute(new FakeQuery());
        
        var logs = _logger.GetLogItems(LogLevel.Information);
        Assert.Contains(logs, l => l.Message.Contains("Query finished"));
    }

    [Fact]
    public async Task WhenQueryFailsThenFailureOfQueryIsLogged()
    {
        await Assert.ThrowsAsync<Exception>(() => _messenger.Execute(new FailingQuery()));
        
        var logs = _logger.GetLogItems(LogLevel.Error);
        Assert.Contains(logs, l => l.Message.Contains("Query failed"));
    }
}