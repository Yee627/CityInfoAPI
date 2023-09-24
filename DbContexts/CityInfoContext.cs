using CityInfoAPI.Entities;
using Microsoft.EntityFrameworkCore;
namespace CityInfoAPI.DbContexts;

public class CityInfoContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<PointOfInterest> PointOfInterests { get; set; }

    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
    {
        
    }
}