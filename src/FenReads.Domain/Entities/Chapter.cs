using FenReads.Domain.Common;

namespace FenReads.Domain.Entities;

public class Chapter : BaseEntity
{
    public Guid VolumeId { get; set; }
    public string? ChapterNumber { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public int PageCount { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Language { get; set; }
    public string? TranslatorGroup { get; set; }
    
    // Navigation properties
    public virtual Volume Volume { get; set; } = null!;
    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();
    public virtual ICollection<ReadingProgress> ReadingProgress { get; set; } = new List<ReadingProgress>();
    public virtual ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}