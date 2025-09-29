using FenReads.Domain.Common;
using FenReads.Domain.Enums;

namespace FenReads.Domain.Entities;

public class Work : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? AlternativeTitle { get; set; }
    public string? Description { get; set; }
    public WorkType Type { get; set; }
    public WorkStatus Status { get; set; } = WorkStatus.Unknown;
    public string? CoverImagePath { get; set; }
    public string? Author { get; set; }
    public string? Artist { get; set; }
    public int? Year { get; set; }
    public string? Language { get; set; } = "fr";
    public string? OriginalLanguage { get; set; }
    public bool IsAdult { get; set; }
    public string? ExternalId { get; set; }
    public string? ExternalSource { get; set; }

    // Navigation properties
    public virtual ICollection<Volume> Volumes { get; set; } = new List<Volume>();
    public virtual ICollection<WorkTag> Tags { get; set; } = new List<WorkTag>();
    public virtual ICollection<UserLibrary> UserLibraries { get; set; } = new List<UserLibrary>();
    public virtual WorkMetadata? Metadata { get; set; }
}