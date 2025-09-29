using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder.ToTable("Bookmarks");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.UserId)
            .IsRequired();

        builder.Property(b => b.ChapterId)
            .IsRequired();

        builder.Property(b => b.PageNumber)
            .IsRequired();

        builder.Property(b => b.ScrollPosition)
            .HasColumnType("decimal(10,2)");

        builder.Property(b => b.Title)
            .HasMaxLength(500);

        builder.Property(b => b.Notes)
            .HasMaxLength(2000);

        builder.Property(b => b.Color)
            .HasMaxLength(7);

        // Indexes
        builder.HasIndex(b => b.UserId);
        builder.HasIndex(b => b.ChapterId);

        // Relationships
        builder.HasOne<Chapter>()
            .WithMany(c => c.Bookmarks)
            .HasForeignKey(b => b.ChapterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}