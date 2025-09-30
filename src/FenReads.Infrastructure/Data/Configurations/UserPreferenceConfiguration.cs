using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
{
    public void Configure(EntityTypeBuilder<UserPreference> builder)
    {
        builder.ToTable("UserPreferences");

        builder.HasKey(up => up.Id);

        builder.Property(up => up.UserId)
            .IsRequired();

        builder.Property(up => up.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(up => up.Value)
            .IsRequired()
            .HasMaxLength(1000);

        // Indexes
        builder.HasIndex(up => up.UserId);
        builder.HasIndex(up => new { up.UserId, up.Key })
            .IsUnique();
    }
}