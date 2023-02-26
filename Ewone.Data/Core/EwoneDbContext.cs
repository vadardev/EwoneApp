using Ewone.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ewone.Data.Core;

public sealed class EwoneDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Module> Modules { get; set; } = null!;
    public DbSet<Word> Words { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "host=pg;port=5432;database=ewonedb;username=postgres;password=1";

            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var now = DateTime.UtcNow;

        var module = new Module
        {
            Id = 1,
            Name = "Default",
            CreateDate = now,
        };

        modelBuilder.Entity<Module>().HasData(module);

        var word1 = new Word
        {
            Id = 1,
            CreateDate = now,
            Name = "book",
        };
        var word2 = new Word
        {
            Id = 2,
            CreateDate = now,
            Name = "apple",
        };
        var word3 = new Word
        {
            Id = 3,
            CreateDate = now,
            Name = "cat",
        };

        modelBuilder.Entity<Word>().HasData(new List<Word>
        {
            word1, word2, word3,
        });

        modelBuilder.Entity<Card>().HasData(new List<Card>
        {
            new Card
            {
                Id = 1,
                CreateDate = now,
                WordId = word1.Id,
                Definition = "a written text that can be published in printed or electronic form",
                ImageUrl = "https://dictionary.cambridge.org/images/thumb/book_noun_001_01679.jpg",
                Examples = new List<string>
                {
                    "We are reading a different book this week"
                },
                ModuleId = module.Id,
            },
            new Card
            {
                Id = 2,
                CreateDate = now,
                WordId = word2.Id,
                Definition = "a round fruit with firm, white flesh and a green, red, or yellow skin",
                ImageUrl = "https://dictionary.cambridge.org/images/thumb/apple_noun_001_00650.jpg",
                Examples = new List<string>
                {
                    "The apple tree at the bottom of the garden is beginning to blossom"
                },
                ModuleId = module.Id,
            },
            new Card
            {
                Id = 3,
                CreateDate = now,
                WordId = word3.Id,
                Definition = "a small animal with fur, four legs, a tail, and claws, usually kept as a pet or for catching mice",
                ImageUrl = "https://dictionary.cambridge.org/images/thumb/cat_noun_001_02368.jpg",
                Examples = new List<string>
                {
                    "My cat likes dozing in front of the fire"
                },
                ModuleId = module.Id,
            },
        });
    }
}