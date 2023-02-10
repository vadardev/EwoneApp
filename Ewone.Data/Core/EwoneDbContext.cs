using Ewone.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Core;

public sealed class EwoneDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Word> Words { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "host=localhost;port=5432;database=ewonedb;username=postgres;password=1";

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}