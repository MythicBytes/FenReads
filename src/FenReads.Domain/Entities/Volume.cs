using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class Volume : BaseEntity
{
    public Guid WorkId { get; set; }
    public string? VolumeNumber { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? CoverImagePath { get; set; }
    public int SortOrder { get; set; }
    public DateTime? ReleaseDate { get; set; }

    // Navigation properties
    public virtual Work Work { get; set; } = null!;
    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}