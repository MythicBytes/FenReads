using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class UserLibraryConfiguration : IEntityTypeConfiguration<UserLibrary>
{
    public void Configure(EntityTypeBuilder<UserLibrary> builder)
    {
        builder.ToTable("UserLibraries");

        builder.HasKey(ul => ul.Id);

        builder.Property(ul => ul.UserId)
            .IsRequired();

        builder.Property(ul => ul.WorkId)
            .IsRequired();

        builder.Property(ul => ul.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(ul => ul.Rating)
            .IsRequired(false);

        builder.Property(ul => ul.Notes)
            .HasMaxLength(2000);

        builder.Property(ul => ul.IsFavorite)
            .IsRequired()
            .HasDefaultValue(false);

        // Indexes
        builder.HasIndex(ul => ul.UserId);
        builder.HasIndex(ul => ul.WorkId);
        builder.HasIndex(ul => new { ul.UserId, ul.WorkId })
            .IsUnique();

        // Relationships
        builder.HasOne<Work>()
            .WithMany()
            .HasForeignKey(ul => ul.WorkId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}