using Modern.App.Aspire.Host;

var builder = DistributedApplication.CreateBuilder(args);

var usersApi = builder.AddProject<Projects.Modern_App_Users_Api_Host>(ModernAppResourceName.UsersApi)
    .WithDeveloperCertificateTrust(trust: true)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Modern_App_Users_Web_Host>(ModernAppResourceName.UsersWeb)
    .WithReference(usersApi)
    .WithDeveloperCertificateTrust(trust: true)
    .WithExternalHttpEndpoints();

builder.Build().Run();