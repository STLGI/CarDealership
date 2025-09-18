using CarDealership.Models;
using Microsoft.EntityFrameworkCore;

public class DealershipDbContext : DbContext
{
    private readonly IConfiguration _appConfig;

    public DealershipDbContext(DbContextOptions<DealershipDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Company> Companies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Manufacturer)
            .WithMany();
    }
}