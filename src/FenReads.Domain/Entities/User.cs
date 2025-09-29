using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAdmin { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    // Navigation properties
    public virtual ICollection<UserLibrary> Libraries { get; set; } = new List<UserLibrary>();
    public virtual ICollection<ReadingProgress> ReadingProgress { get; set; } = new List<ReadingProgress>();
    public virtual ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
    public virtual ICollection<UserPreference> Preferences { get; set; } = new List<UserPreference>();
}