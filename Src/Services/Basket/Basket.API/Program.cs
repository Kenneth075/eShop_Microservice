using BuildingBlock.Behaviour;
using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var assembly = typeof(Program).Assembly;

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DbCon")!);
}).UseLightweightSessions();

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
var app = builder.Build();

//Configure HTTP request pipeline.

app.MapCarter();

app.Run();
