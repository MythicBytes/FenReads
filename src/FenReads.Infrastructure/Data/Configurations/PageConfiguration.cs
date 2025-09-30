using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("Pages");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ChapterId)
            .IsRequired();

        builder.Property(p => p.PageNumber)
            .IsRequired();

        builder.Property(p => p.FilePath)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(p => p.ThumbnailPath)
            .HasMaxLength(1000);

        builder.Property(p => p.MimeType)
            .HasMaxLength(100);

        // Index
        builder.HasIndex(p => new { p.ChapterId, p.PageNumber })
            .IsUnique();
    }
}