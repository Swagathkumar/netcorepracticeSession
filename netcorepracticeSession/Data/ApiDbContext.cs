using Microsoft.EntityFrameworkCore;
using netcorepracticeSession.Models;

namespace netcorepracticeSession.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Property> properties { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SQL8004.site4now.net;Initial Catalog=db_a96d21_netcoresessiondb;User Id=db_a96d21_netcoresessiondb_admin;Password=Swa261295;");

            //optionsBuilder.UseSqlServer(@"Server =tcp:netcoresession.database.windows.net,1433;Initial Catalog=netcorepracticeSessionDB;Persist Security Info=False;User ID=swagath;Password=Swa261295;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }
}
