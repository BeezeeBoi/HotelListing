using HotelListing.API.Data.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data;

public class HotelListingDbContext : IdentityDbContext<User>
{
    public HotelListingDbContext(DbContextOptions options) : base (options)
    {
        
    }

    public DbSet<Region> Regions { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new IdentityRoleConfig());
        modelBuilder.ApplyConfiguration(new CountryConfig());
        modelBuilder.ApplyConfiguration(new HotelConfig());
        modelBuilder.ApplyConfiguration(new HotelConfig());
    }
}