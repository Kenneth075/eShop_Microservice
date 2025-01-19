var builder = WebApplication.CreateBuilder(args);

//Add services to container

var app = builder.Build();

//Configure Http Pipeline

app.Run();
