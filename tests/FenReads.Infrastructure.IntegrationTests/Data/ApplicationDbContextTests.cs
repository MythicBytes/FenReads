using FenReads.Domain.Entities;
using FenReads.Domain.Enums;
using FenReads.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Testcontainers.PostgreSql;
using Xunit;

namespace FenReads.Infrastructure.IntegrationTests.Data;

public class ApplicationDbContextTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithDatabase("fenreads_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private ApplicationDbContext _context = null!;

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_postgresContainer.GetConnectionString())
            .AddInterceptors(new FenReads.Infrastructure.Data.Interceptors.AuditableEntityInterceptor())
            .Options;

        _context = new ApplicationDbContext(options);
        await _context.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _postgresContainer.DisposeAsync();
    }

    [Fact]
    public async Task CanCreateAndRetrieveUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = "hashedpassword",
            IsActive = true,
            IsAdmin = false
        };

        // Act
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var retrievedUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == "testuser");

        // Assert
        retrievedUser.ShouldNotBeNull();
        retrievedUser.Email.ShouldBe("test@example.com");
        retrievedUser.IsActive.ShouldBeTrue();
        retrievedUser.CreatedAt.ShouldBeGreaterThan(DateTime.MinValue);
    }

    [Fact]
    public async Task CanCreateWorkWithVolumesAndChapters()
    {
        // Arrange
        var work = new Work
        {
            Title = "One Piece",
            Type = WorkType.Manga,
            Status = WorkStatus.Ongoing,
            Author = "Eiichiro Oda",
            Language = "fr"
        };

        var volume = new Volume
        {
            WorkId = work.Id,
            VolumeNumber = "1",
            Title = "Volume 1",
            SortOrder = 1
        };

        var chapter = new Chapter
        {
            VolumeId = volume.Id,
            ChapterNumber = "1",
            Title = "Chapitre 1",
            SortOrder = 1,
            PageCount = 20
        };

        work.Volumes.Add(volume);
        volume.Chapters.Add(chapter);

        // Act
        await _context.Works.AddAsync(work);
        await _context.SaveChangesAsync();

        var retrievedWork = await _context.Works
            .Include(w => w.Volumes)
                .ThenInclude(v => v.Chapters)
            .FirstOrDefaultAsync(w => w.Title == "One Piece");

        // Assert
        retrievedWork.ShouldNotBeNull();
        retrievedWork.Volumes.Count.ShouldBe(1);
        retrievedWork.Volumes.First().Chapters.Count.ShouldBe(1);
    }

    [Fact]
    public async Task AuditFieldsAreAutomaticallySet()
    {
        // Arrange
        var tag = new Tag
        {
            Name = "Action",
            Category = "Genre"
        };

        // Act
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();

        // Clear tracking to force reload from database
        _context.ChangeTracker.Clear();
        var retrievedTag = await _context.Tags.FindAsync(tag.Id);

        // Assert
        retrievedTag.ShouldNotBeNull();
        retrievedTag.CreatedAt.ShouldNotBe(default(DateTime));
        retrievedTag.UpdatedAt.ShouldNotBe(default(DateTime));
        retrievedTag.CreatedBy.ShouldBe("system");
        retrievedTag.UpdatedBy.ShouldBe("system");
    }

    [Fact]
    public async Task CanCreateReadingProgressForUser()
    {
        // Arrange
        var user = new User
        {
            Username = "reader",
            Email = "reader@example.com",
            PasswordHash = "hash",
            IsActive = true
        };

        var work = new Work
        {
            Title = "Test Manga",
            Type = WorkType.Manga,
            Status = WorkStatus.Ongoing
        };

        var volume = new Volume
        {
            VolumeNumber = "1",
            SortOrder = 1
        };

        var chapter = new Chapter
        {
            ChapterNumber = "1",
            SortOrder = 1,
            PageCount = 50
        };

        work.Volumes.Add(volume);
        volume.Chapters.Add(chapter);

        // Act
        await _context.Users.AddAsync(user);
        await _context.Works.AddAsync(work);
        await _context.SaveChangesAsync();

        // Now create the reading progress with the persisted chapter ID
        var progress = new ReadingProgress
        {
            UserId = user.Id,
            ChapterId = chapter.Id,
            LastPageRead = 25,
            TotalPages = 50,
            ProgressPercentage = 50m,
            LastReadAt = DateTime.UtcNow,
            IsCompleted = false
        };

        await _context.ReadingProgresses.AddAsync(progress);
        await _context.SaveChangesAsync();

        var retrievedProgress = await _context.ReadingProgresses
            .FirstOrDefaultAsync(rp => rp.UserId == user.Id);

        // Assert
        retrievedProgress.ShouldNotBeNull();
        retrievedProgress.LastPageRead.ShouldBe(25);
        retrievedProgress.ProgressPercentage.ShouldBe(50m);
        retrievedProgress.IsCompleted.ShouldBeFalse();
    }
}