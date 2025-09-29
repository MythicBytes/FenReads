using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class WorkMetadataConfiguration : IEntityTypeConfiguration<WorkMetadata>
{
    public void Configure(EntityTypeBuilder<WorkMetadata> builder)
    {
        builder.ToTable("WorkMetadata");

        builder.HasKey(wm => wm.Id);

        builder.Property(wm => wm.WorkId)
            .IsRequired();

        builder.Property(wm => wm.Synopsis)
            .HasColumnType("text");

        builder.Property(wm => wm.Publisher)
            .HasMaxLength(255);

        builder.Property(wm => wm.Magazine)
            .HasMaxLength(255);

        builder.Property(wm => wm.Demographic)
            .HasMaxLength(50);

        builder.Property(wm => wm.AgeRating)
            .HasMaxLength(20);

        builder.Property(wm => wm.Website)
            .HasMaxLength(500);

        builder.Property(wm => wm.Twitter)
            .HasMaxLength(255);

        builder.Property(wm => wm.ScrapingSource)
            .HasMaxLength(100);

        // Index
        builder.HasIndex(wm => wm.WorkId)
            .IsUnique();
    }
}