using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class Bookmark : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ChapterId { get; set; }
    public int PageNumber { get; set; }
    public decimal? ScrollPosition { get; set; }
    public string? Title { get; set; }
    public string? Notes { get; set; }
    public string? Color { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Chapter Chapter { get; set; } = null!;
}