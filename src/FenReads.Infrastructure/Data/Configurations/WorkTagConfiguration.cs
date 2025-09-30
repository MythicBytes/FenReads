using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class WorkTagConfiguration : IEntityTypeConfiguration<WorkTag>
{
    public void Configure(EntityTypeBuilder<WorkTag> builder)
    {
        builder.ToTable("WorkTags");

        builder.HasKey(wt => wt.Id);

        builder.Property(wt => wt.WorkId)
            .IsRequired();

        builder.Property(wt => wt.TagId)
            .IsRequired();

        // Composite index
        builder.HasIndex(wt => new { wt.WorkId, wt.TagId })
            .IsUnique();

        // Relationships
        builder.HasOne<Tag>()
            .WithMany()
            .HasForeignKey(wt => wt.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}