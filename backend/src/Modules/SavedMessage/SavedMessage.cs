using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interface;
using MyApp.Modules.SavedMessage.Infrastructure.Database;

namespace MyApp.Modules.SavedMessage;

public static class SavedMessage
{
    // test
    public static IServiceCollection AddSavedMessage(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<SavedMessageDbContext>(option =>
        {
            option.UseSqlite(connectionString);
        });

        services.AddScoped<IModuleInitializer, SavedMessageInitializer>();

        return services;
    }

    public static void MapSavedMessage(this IEndpointRouteBuilder app) {
        app.MapGet("saved-message", (SavedMessageDbContext db) => {
            return db.SavedMessages.ToList();
        });
    }
}


public class SavedMessageInitializer(SavedMessageDbContext db) : IModuleInitializer
{

    public async Task InitializeAsync()
    {
        await db.Database.MigrateAsync();

        await SavedMessageDbFeeder.FeedAsync(db);

        foreach (var item in db.SavedMessages)
            Console.WriteLine(item.Message);
    }

}
