using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
}).UseLightweightSessions();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var martenStore = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
//    await martenStore.Storage.ApplyAllConfiguredChangesToDatabaseAsync();
//}


//Configure HTTP request pipeline.

app.MapCarter();

app.Run();
