using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FenReads.Infrastructure.Data.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Seed Tags
        if (!await context.Tags.AnyAsync())
        {
            var tags = new List<Tag>
            {
                // Genres
                new() { Id = Guid.NewGuid(), Name = "Action", Category = "Genre", Description = "Action et aventure" },
                new() { Id = Guid.NewGuid(), Name = "Romance", Category = "Genre", Description = "Romance" },
                new() { Id = Guid.NewGuid(), Name = "Fantasy", Category = "Genre", Description = "Fantaisie" },
                new() { Id = Guid.NewGuid(), Name = "Sci-Fi", Category = "Genre", Description = "Science-fiction" },
                new() { Id = Guid.NewGuid(), Name = "Horror", Category = "Genre", Description = "Horreur" },
                new() { Id = Guid.NewGuid(), Name = "Mystery", Category = "Genre", Description = "Mystère" },
                new() { Id = Guid.NewGuid(), Name = "Comedy", Category = "Genre", Description = "Comédie" },
                new() { Id = Guid.NewGuid(), Name = "Drama", Category = "Genre", Description = "Drame" },
                new() { Id = Guid.NewGuid(), Name = "Slice of Life", Category = "Genre", Description = "Tranche de vie" },

                // Demographics
                new() { Id = Guid.NewGuid(), Name = "Shonen", Category = "Demographic", Description = "Pour jeunes garçons" },
                new() { Id = Guid.NewGuid(), Name = "Shojo", Category = "Demographic", Description = "Pour jeunes filles" },
                new() { Id = Guid.NewGuid(), Name = "Seinen", Category = "Demographic", Description = "Pour jeunes adultes (H)" },
                new() { Id = Guid.NewGuid(), Name = "Josei", Category = "Demographic", Description = "Pour jeunes adultes (F)" },
                new() { Id = Guid.NewGuid(), Name = "Kodomo", Category = "Demographic", Description = "Pour enfants" },

                // Themes
                new() { Id = Guid.NewGuid(), Name = "Isekai", Category = "Theme", Description = "Monde parallèle" },
                new() { Id = Guid.NewGuid(), Name = "Martial Arts", Category = "Theme", Description = "Arts martiaux" },
                new() { Id = Guid.NewGuid(), Name = "School Life", Category = "Theme", Description = "Vie scolaire" },
                new() { Id = Guid.NewGuid(), Name = "Sports", Category = "Theme", Description = "Sports" },
                new() { Id = Guid.NewGuid(), Name = "Supernatural", Category = "Theme", Description = "Surnaturel" },
                new() { Id = Guid.NewGuid(), Name = "Historical", Category = "Theme", Description = "Historique" }
            };

            await context.Tags.AddRangeAsync(tags);
            await context.SaveChangesAsync();
        }

        // Seed Admin User
        if (!await context.Users.AnyAsync(u => u.Email == "admin@fenreads.local"))
        {
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@fenreads.local",
                // Password: "Admin123!" hashed avec BCrypt
                PasswordHash = "$2a$11$4ZxJm0vGKx5b8yH7QY5rR.x8mDx0YQJvY5xN2m8Z6xQ8mX5jN2m8u",
                DisplayName = "Administrator",
                IsActive = true,
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }
}