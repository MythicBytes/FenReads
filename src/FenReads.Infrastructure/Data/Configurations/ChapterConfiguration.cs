using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.ToTable("Chapters");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.VolumeId)
            .IsRequired();

        builder.Property(c => c.ChapterNumber)
            .HasMaxLength(50);

        builder.Property(c => c.Title)
            .HasMaxLength(500);

        builder.Property(c => c.Description)
            .HasColumnType("text");

        builder.Property(c => c.Language)
            .HasMaxLength(10);

        builder.Property(c => c.TranslatorGroup)
            .HasMaxLength(255);

        // Indexes
        builder.HasIndex(c => new { c.VolumeId, c.ChapterNumber })
            .IsUnique();

        // Relationships
        builder.HasMany(c => c.Pages)
            .WithOne(p => p.Chapter)
            .HasForeignKey(p => p.ChapterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}