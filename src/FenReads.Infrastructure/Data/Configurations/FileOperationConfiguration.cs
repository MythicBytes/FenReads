using FenReads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FenReads.Infrastructure.Data.Configurations;

public class FileOperationConfiguration : IEntityTypeConfiguration<FileOperation>
{
    public void Configure(EntityTypeBuilder<FileOperation> builder)
    {
        builder.ToTable("FileOperations");

        builder.HasKey(fo => fo.Id);

        builder.Property(fo => fo.OperationType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(fo => fo.SourcePath)
            .HasMaxLength(1000);

        builder.Property(fo => fo.DestinationPath)
            .HasMaxLength(1000);

        builder.Property(fo => fo.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(fo => fo.ErrorMessage)
            .HasColumnType("text");

        builder.Property(fo => fo.WorkId)
            .IsRequired(false);

        builder.Property(fo => fo.VolumeId)
            .IsRequired(false);

        builder.Property(fo => fo.ChapterId)
            .IsRequired(false);

        // Indexes
        builder.HasIndex(fo => fo.Status);
        builder.HasIndex(fo => fo.CreatedAt);
    }
}