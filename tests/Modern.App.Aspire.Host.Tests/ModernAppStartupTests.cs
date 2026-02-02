using Modern.App.Aspire.Host.Tests.Support;

namespace Modern.App.Aspire.Host.Tests;

public class ModernAppStartupTests
{
    [Fact]
    public async Task WhenAspireHostIsStartedThenUsersApiIsStarted()
    {
        await using var app = await ModernAppTestingHost.StartAsync();
        using var client = app.CreateHttpClient(ModernAppResourceName.UsersApi);
        var response = await client.GetAsync("/.health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task WhenAspireHostIsStartedThenUsersWebIsStarted()
    {
        await using var app = await ModernAppTestingHost.StartAsync();
        using var client = app.CreateHttpClient(ModernAppResourceName.UsersWeb);
        var response = await client.GetAsync("/.health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
