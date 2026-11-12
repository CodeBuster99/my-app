using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Modules.SavedMessage.Infrastructure.Database;

public class SavedMessageDbContext(DbContextOptions<SavedMessageDbContext> options) : DbContext(options)
{
    public DbSet<SavedMessageModel> SavedMessages { get; set; }
}

public class SavedMessageModel
{
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
}

public static class SavedMessageDbFeeder
{
    public static async Task FeedAsync(SavedMessageDbContext db)
    {
        if (db.SavedMessages.Any())
            return;

        Console.WriteLine("feeding");
        db.SavedMessages.AddRange([
            new SavedMessageModel() { Id = Guid.NewGuid(), Message = "abc"},
            new SavedMessageModel() { Id = Guid.NewGuid(), Message = "def"}
        ]);

        await db.SaveChangesAsync();
    }
}
