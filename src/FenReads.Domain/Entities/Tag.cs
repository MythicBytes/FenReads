using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; } // genre, publisher, theme, etc.
    public string? Description { get; set; }
    public string? Color { get; set; }
    
    // Navigation properties
    public virtual ICollection<WorkTag> Works { get; set; } = new List<WorkTag>();
}