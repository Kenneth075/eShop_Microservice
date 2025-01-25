using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to container
builder.Services
    .AddApplicationService()
    .AddInfrastructureService(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//Configure Http Pipeline

app.Run();
