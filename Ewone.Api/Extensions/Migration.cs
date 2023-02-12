using Ewone.Data.Core;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Api.Extensions;

public static class Migration
{
    public static async Task RunMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<EwoneDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}