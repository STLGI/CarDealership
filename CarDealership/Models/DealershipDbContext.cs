using Microsoft.EntityFrameworkCore;



namespace CarDealership.Models
{
    public class DealershipDbContext : DbContext
    {
        IConfiguration appConfig;
        public DealershipDbContext(IConfiguration config)
        {
            appConfig = config;
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(appConfig.GetConnectionString("DealershipDbLocalConnection"));


        }
    }
}