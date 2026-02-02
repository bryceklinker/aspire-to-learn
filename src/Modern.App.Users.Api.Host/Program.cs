var builder = WebApplication.CreateBuilder(args);
builder.AddAspireServiceDefaults();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapAspireDefaultEndpoints();
app.UseHttpsRedirection();
app.Run();
