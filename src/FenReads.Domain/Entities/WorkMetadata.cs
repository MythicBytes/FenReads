using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class WorkMetadata : BaseEntity
{
    public Guid WorkId { get; set; }
    public string? Synopsis { get; set; }
    public string? Publisher { get; set; }
    public string? Magazine { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? ChapterCount { get; set; }
    public int? VolumeCount { get; set; }
    public string? Demographic { get; set; }
    public string? AgeRating { get; set; }
    public string? Website { get; set; }
    public string? Twitter { get; set; }
    public DateTime? LastScrapedAt { get; set; }
    public string? ScrapingSource { get; set; }

    // Navigation property
    public virtual Work Work { get; set; } = null!;
}