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

        public DbSet<station_masters> station_master { get; set; }
        public DbSet<data_configs> data_config { get; set; }
        public DbSet<cards> card { get; set; }


        /*        public DbSet<file_paths> file_path { get; set; }
                public DbSet<images> image { get; set; }
                public DbSet<user_managers> user_manager { get; set; }*/
    }
}
