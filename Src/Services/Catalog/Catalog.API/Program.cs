using BuildingBlock.Behaviour;
using Carter;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DbCon")!);
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
}).UseLightweightSessions();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var martenStore = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
    await martenStore.Storage.ApplyAllConfiguredChangesToDatabaseAsync();
}


//Configure HTTP request pipeline.

app.MapCarter();

app.Run();
