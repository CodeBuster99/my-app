using MyApp.Application.Interface;
using MyApp.Modules.SavedMessage;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine("ConnnectionStrings: " + connectionString);
ArgumentException.ThrowIfNullOrEmpty(connectionString);

builder.Services
    .AddSavedMessage(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

using (var scope = app.Services.CreateScope())
{
    var initializers = scope.ServiceProvider.GetServices<IModuleInitializer>();
    foreach (var initializer in initializers)
        await initializer.InitializeAsync();
}

app.MapSavedMessage();

app.Run();
