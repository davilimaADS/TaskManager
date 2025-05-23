using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.HasIndex(x => x.Email)
                .IsUnique();
            
            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("nvarchar(150)")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnType("nvarchar(200)");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(x => x.Projects)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            builder.HasMany(x => x.AssignedTasks)
                .WithOne(p => p.AssignedUser)
                .HasForeignKey(p => p.AssignedUserId);
                
        }
    }
}
