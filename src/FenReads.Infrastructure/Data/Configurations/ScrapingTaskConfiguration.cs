using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class ScrapingTaskConfiguration : IEntityTypeConfiguration<ScrapingTask>
{
    public void Configure(EntityTypeBuilder<ScrapingTask> builder)
    {
        builder.ToTable("ScrapingTasks");

        builder.HasKey(st => st.Id);

        builder.Property(st => st.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(st => st.Source)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(st => st.Url)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(st => st.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(st => st.Priority)
            .IsRequired();

        builder.Property(st => st.RetryCount)
            .IsRequired();

        builder.Property(st => st.WorkId)
            .IsRequired(false);

        builder.Property(st => st.ErrorMessage)
            .HasColumnType("text");

        builder.Property(st => st.ResultData)
            .HasColumnType("text");

        // Indexes
        builder.HasIndex(st => st.Status);
        builder.HasIndex(st => st.ScheduledAt);
        builder.HasIndex(st => st.WorkId);

        // Relationships
        builder.HasOne<Work>()
            .WithMany()
            .HasForeignKey(st => st.WorkId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}