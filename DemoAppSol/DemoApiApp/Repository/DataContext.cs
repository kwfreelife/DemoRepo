using DemoApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoApiApp.Repository
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            //options.UseInMemoryDatabase("TestDb");
            //options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<UserDemo> Users { get; set; }
    }
}
