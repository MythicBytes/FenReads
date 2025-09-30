using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.ToTable("Works");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(w => w.AlternativeTitle)
            .HasMaxLength(500);

        builder.Property(w => w.Description)
            .HasColumnType("text");

        builder.Property(w => w.Author)
            .HasMaxLength(255);

        builder.Property(w => w.Artist)
            .HasMaxLength(255);

        builder.Property(w => w.CoverImagePath)
            .HasMaxLength(1000);

        builder.Property(w => w.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(w => w.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(w => w.Language)
            .HasMaxLength(10);

        builder.Property(w => w.OriginalLanguage)
            .HasMaxLength(10);

        builder.Property(w => w.ExternalId)
            .HasMaxLength(255);

        builder.Property(w => w.ExternalSource)
            .HasMaxLength(100);

        builder.Property(w => w.IsAdult)
            .IsRequired()
            .HasDefaultValue(false);

        // Indexes
        builder.HasIndex(w => w.Title);
        builder.HasIndex(w => w.Type);
        builder.HasIndex(w => w.Status);
        builder.HasIndex(w => w.Author);

        // Relationships
        builder.HasMany(w => w.Volumes)
            .WithOne(v => v.Work)
            .HasForeignKey(v => v.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Tags)
            .WithOne()
            .HasForeignKey(wt => wt.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.Metadata)
            .WithOne(wm => wm.Work)
            .HasForeignKey<WorkMetadata>(wm => wm.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.UserLibraries)
            .WithOne(ul => ul.Work)
            .HasForeignKey(ul => ul.WorkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}