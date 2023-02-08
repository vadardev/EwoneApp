using Ewone.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Core;

public sealed class EwoneDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public EwoneDbContext(DbContextOptions<EwoneDbContext> options) : base(options)
    {
        Database.Migrate();
    }
}