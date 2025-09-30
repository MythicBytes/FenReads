using FenReads.Domain.Entities;
using FenReads.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace FenReads.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Users
    public DbSet<User> Users => Set<User>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
    public DbSet<UserLibrary> UserLibraries => Set<UserLibrary>();

    // Works
    public DbSet<Work> Works => Set<Work>();
    public DbSet<Volume> Volumes => Set<Volume>();
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<WorkMetadata> WorkMetadata => Set<WorkMetadata>();

    // Tags
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<WorkTag> WorkTags => Set<WorkTag>();

    // Reading Progress
    public DbSet<ReadingProgress> ReadingProgresses => Set<ReadingProgress>();
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();

    // File Operations
    public DbSet<FileOperation> FileOperations => Set<FileOperation>();

    // Scraping
    public DbSet<ScrapingTask> ScrapingTasks => Set<ScrapingTask>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}