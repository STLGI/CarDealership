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
    public DbSet<CarImage> CarImages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Img = "cross.png", Name = "None", SName = "None" },
            new Company { Id = 2, Img = "audi.png", Name = "Audi AG", SName = "Audi" },
            new Company { Id = 3, Img = "mercedes.png", Name = "Mercedes Benz AG", SName = "Mercedes" },
            new Company { Id = 4, Img = "toyota.png", Name = "Toyota Motor Corporation", SName = "Toyota" },
            new Company { Id = 5, Img = "volkswagen.png", Name = "Volkswagen", SName = "Volkswagen" }
);
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Manufacturer)
            .WithMany();
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Images)
            .WithOne(i => i.Car)
            .HasForeignKey(i => i.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}