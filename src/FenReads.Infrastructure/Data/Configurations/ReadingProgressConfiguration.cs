using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class ReadingProgressConfiguration : IEntityTypeConfiguration<ReadingProgress>
{
    public void Configure(EntityTypeBuilder<ReadingProgress> builder)
    {
        builder.ToTable("ReadingProgresses");

        builder.HasKey(rp => rp.Id);

        builder.Property(rp => rp.UserId)
            .IsRequired();

        builder.Property(rp => rp.ChapterId)
            .IsRequired();

        builder.Property(rp => rp.LastPageRead)
            .IsRequired();

        builder.Property(rp => rp.TotalPages)
            .IsRequired();

        builder.Property(rp => rp.ProgressPercentage)
            .HasColumnType("decimal(5,2)");

        builder.Property(rp => rp.ReadingTime)
            .IsRequired();

        builder.Property(rp => rp.IsCompleted)
            .IsRequired();

        // Indexes
        builder.HasIndex(rp => rp.UserId);
        builder.HasIndex(rp => rp.ChapterId);
        builder.HasIndex(rp => new { rp.UserId, rp.ChapterId })
            .IsUnique();

        // Relationships
        builder.HasOne<Chapter>()
            .WithMany(c => c.ReadingProgress)
            .HasForeignKey(rp => rp.ChapterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}