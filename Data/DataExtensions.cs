using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1.Data;

public static class DataExtensions
{
    // Extension method to apply any pending migrations to the database
    public static void MigrateDb(this WebApplication app)
    {
        // Create a new scope for resolving services
        using var scope = app.Services.CreateScope();

        // Get the GameStoreContext service from the service provider
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        // Apply any pending migrations to the database
        dbContext.Database.Migrate();
    }
}
