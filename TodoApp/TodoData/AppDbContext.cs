using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.TodoData
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() // <-- Added for design-time support
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost,1433;Initial Catalog=todo-db;User Id=sa;Password=Rana@1234;TrustServerCertificate=True;"
                );
            }
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.IsActive).HasDefaultValue(false);

                entity.HasMany(c => c.Items)
                      .WithOne(i => i.Category)
                      .HasForeignKey(i => i.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new { Id = 1, Name = "Work", IsActive = true },
                    new { Id = 2, Name = "Personal", IsActive = true },
                    new { Id = 3, Name = "Shopping", IsActive = false }
                );
            });

            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.ToTable("TodoItems");
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Id).ValueGeneratedOnAdd();
                entity.Property(i => i.Title).IsRequired().HasMaxLength(200);
                entity.Property(i => i.Description).HasMaxLength(500);
                entity.Property(i => i.CreateDate).HasDefaultValueSql("GETDATE()");
                entity.Property(i => i.Status).HasDefaultValue(TodoStatus.New);

                entity.HasOne(i => i.Category)
                      .WithMany(c => c.Items)
                      .HasForeignKey(i => i.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
