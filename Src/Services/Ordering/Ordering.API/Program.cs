using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.DatabaseExtension;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services
    .AddApplicationService()
    .AddInfrastructureService(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//Configure Http Pipeline

if (app.Environment.IsDevelopment())
{
    await app.DatabaseInitialization();
}

app.Run();
