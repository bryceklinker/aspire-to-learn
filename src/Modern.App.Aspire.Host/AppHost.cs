using Modern.App.Aspire.Host;

var builder = DistributedApplication.CreateBuilder(args);

var usersApi = builder.AddProject<Projects.Modern_App_Users_Api_Host>(ModernAppResourceName.UsersApi)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Modern_App_Users_Web_Host>(ModernAppResourceName.UsersWeb)
    .WithReference(usersApi)
    .WithExternalHttpEndpoints();

builder.Build().Run();
