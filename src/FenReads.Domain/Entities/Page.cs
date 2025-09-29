using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class Page : BaseEntity
{
    public Guid ChapterId { get; set; }
    public int PageNumber { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? ThumbnailPath { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public long FileSize { get; set; }
    public string? MimeType { get; set; }

    // Navigation property
    public virtual Chapter Chapter { get; set; } = null!;
}