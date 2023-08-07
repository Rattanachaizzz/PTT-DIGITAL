using Microsoft.EntityFrameworkCore;
using WebSupportTeam.Models;

namespace WebSupportTeam.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("support");
        }

        public DbSet<File_path> file_paths { get; set; }
        public DbSet<Images> images { get; set; }
        public DbSet<Station_master> Station_masters { get; set; }
        public DbSet<Users_manager> Users_managers { get; set; }
    }
}
