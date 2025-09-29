using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class UserPreference : BaseEntity
{
    public Guid UserId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    
    // Navigation property
    public virtual User User { get; set; } = null!;
}