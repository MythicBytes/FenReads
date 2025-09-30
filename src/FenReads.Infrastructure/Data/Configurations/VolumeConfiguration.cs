using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class VolumeConfiguration : IEntityTypeConfiguration<Volume>
{
    public void Configure(EntityTypeBuilder<Volume> builder)
    {
        builder.ToTable("Volumes");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.WorkId)
            .IsRequired();

        builder.Property(v => v.VolumeNumber)
            .HasMaxLength(50);

        builder.Property(v => v.Title)
            .HasMaxLength(500);

        builder.Property(v => v.Description)
            .HasColumnType("text");

        builder.Property(v => v.CoverImagePath)
            .HasMaxLength(1000);

        builder.Property(v => v.SortOrder)
            .IsRequired();

        // Indexes
        builder.HasIndex(v => new { v.WorkId, v.VolumeNumber, v.SortOrder });

        // Relationships
        builder.HasMany(v => v.Chapters)
            .WithOne(c => c.Volume)
            .HasForeignKey(c => c.VolumeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}