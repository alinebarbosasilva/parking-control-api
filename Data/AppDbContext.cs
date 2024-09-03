using Microsoft.EntityFrameworkCore;

namespace ParkingControl.Data;

public class AppDbContext : DbContext
{
   public DbSet<ParkingRegistration.ParkingRegistration> ParkingRegistrations { get; set; }
   public DbSet<PriceTable.PriceTable> PricesTable { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Data.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}