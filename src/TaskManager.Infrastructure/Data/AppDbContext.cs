using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {
        }
       public DbSet<User> Users { get; set; }
       public DbSet<TaskItem> Tasks { get; set; }
       public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
