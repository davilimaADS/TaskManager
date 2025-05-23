using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Infrastructure.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Description)
            .HasColumnType("nvarchar(max)");

            builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

            builder.Property(x => x.DeletedAt)
            .IsRequired(false);

            builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnType("nvarchar(50)")
            .HasDefaultValue(ProjectStatus.InProgress);

            builder.HasOne(x => x.Owner)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}
