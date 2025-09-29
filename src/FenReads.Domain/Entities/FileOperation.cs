using FenReads.Domain.Common;
using FenReads.Domain.Enums;

namespace FenReads.Domain.Entities;

public class FileOperation : BaseEntity
{
    public string OperationType { get; set; } = string.Empty; // Move, Rename, Delete, Import
    public string? SourcePath { get; set; }
    public string? DestinationPath { get; set; }
    public FileOperationStatus Status { get; set; } = FileOperationStatus.Pending;
    public DateTime? ScheduledAt { get; set; }
    public DateTime? ExecutedAt { get; set; }
    public string? ErrorMessage { get; set; }
    public Guid? WorkId { get; set; }
    public Guid? VolumeId { get; set; }
    public Guid? ChapterId { get; set; }

    // Navigation properties
    public virtual Work? Work { get; set; }
    public virtual Volume? Volume { get; set; }
    public virtual Chapter? Chapter { get; set; }
}