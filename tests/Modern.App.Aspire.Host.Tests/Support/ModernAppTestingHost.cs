using Aspire.Hosting;

namespace Modern.App.Aspire.Host.Tests.Support;

public class ModernAppTestingHost : IAsyncDisposable
{
    private readonly DistributedApplication _app;
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);
    
    private ModernAppTestingHost(DistributedApplication app)
    {
        _app = app;
    }

    public HttpClient CreateHttpClient(string resourceName)
    {
        return _app.CreateHttpClient(resourceName);
    }
    
    public static async Task<ModernAppTestingHost> StartAsync()
    {
        var builder = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Modern_App_Aspire_Host>();
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
        });

        var app = await builder.BuildAsync().WaitAsync(DefaultTimeout);
        await app.StartAsync().WaitAsync(DefaultTimeout);
        return new ModernAppTestingHost(app);
    }

    public async ValueTask DisposeAsync()
    {
        await _app.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}