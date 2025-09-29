using FenReads.Domain.Common;
using FenReads.Domain.Enums;

namespace FenReads.Domain.Entities;

public class UserLibrary : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid WorkId { get; set; }
    public ReadingStatus Status { get; set; } = ReadingStatus.NotStarted;
    public int? Rating { get; set; } // 1-10
    public string? Notes { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime? StartedReadingAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Work Work { get; set; } = null!;
}