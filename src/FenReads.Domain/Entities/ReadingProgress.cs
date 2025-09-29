using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class ReadingProgress : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ChapterId { get; set; }
    public int LastPageRead { get; set; }
    public int TotalPages { get; set; }
    public decimal ProgressPercentage { get; set; }
    public DateTime LastReadAt { get; set; }
    public TimeSpan ReadingTime { get; set; }
    public bool IsCompleted { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Chapter Chapter { get; set; } = null!;
}