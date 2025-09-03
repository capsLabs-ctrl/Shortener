using Microsoft.EntityFrameworkCore;
using site_test_task.Models; // замените на своё пространство имён, если другое

namespace site_test_task.Data // замените на своё пространство имён
{
    public class AppDbContext : DbContext
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortUrl>().HasIndex(u => u.ShortCode).IsUnique();
        }
    }
}
