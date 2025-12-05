using Microsoft.EntityFrameworkCore;
using ecology_dotnet.Models;

namespace ecology_dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Complain> Complains { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.HasIndex(e => e.email).IsUnique();

                entity.HasMany(e => e.complains)
                    .WithOne(e => e.user)
                    .HasForeignKey(e => e.userId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.tasks)
                    .WithOne(e => e.user)
                    .HasForeignKey(e => e.userId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Complain entity configuration
            modelBuilder.Entity<Complain>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.title).IsRequired();
                entity.Property(e => e.description).IsRequired();
            });

            // Task entity configuration
            modelBuilder.Entity<Models.Task>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.createdAt).HasDefaultValueSql("NOW()");
                entity.Property(e => e.approved).HasDefaultValue(false);
            });
        }
    }
}
