using Kavsar.Modules.Audit;
using Kavsar.Modules.Membership;
using Kavsar.Modules.Organization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOrganizationModule()
    .AddMembershipModule()
    .AddAuditModule();

var app = builder.Build();

app.Run();
