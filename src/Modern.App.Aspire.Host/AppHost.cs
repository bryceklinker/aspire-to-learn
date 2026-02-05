using Modern.App.Aspire.Host;

var builder = DistributedApplication.CreateBuilder(args);

var usersApi = builder.AddProject<Projects.Modern_App_Users_Api_Host>(ModernAppResourceName.UsersApi)
    .WithExternalHttpEndpoints()
    .WithDeveloperCertificateTrust(trust: true);

builder.AddProject<Projects.Modern_App_Users_Web_Host>(ModernAppResourceName.UsersWeb)
    .WithReference(usersApi)
    .WithExternalHttpEndpoints()
    .WithDeveloperCertificateTrust(trust: true);

builder.Build().Run();
