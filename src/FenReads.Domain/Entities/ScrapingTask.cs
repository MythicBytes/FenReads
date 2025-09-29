using FenReads.Domain.Common;
using FenReads.Domain.Enums;

namespace FenReads.Domain.Entities;

public class ScrapingTask : BaseEntity
{
    public string Source { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public ScrapingType Type { get; set; }
    public ScrapingStatus Status { get; set; } = ScrapingStatus.Pending;
    public int Priority { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int RetryCount { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ResultData { get; set; } // JSON data
    public Guid? WorkId { get; set; }
    
    // Navigation property
    public virtual Work? Work { get; set; }
}