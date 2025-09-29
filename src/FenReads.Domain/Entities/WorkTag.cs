using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class WorkTag : BaseEntity
{
    public Guid WorkId { get; set; }
    public Guid TagId { get; set; }

    // Navigation properties
    public virtual Work Work { get; set; } = null!;
    public virtual Tag Tag { get; set; } = null!;
}