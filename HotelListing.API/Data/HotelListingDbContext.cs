using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options) : base (options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2, 
                    Name = "United States of America", 
                    ShortName = "USA"
                });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Holiday Inn",
                    Address = "George Town",
                    CountryId = 1,
                    Rating = 3.8
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hilton",
                    Address = "Knoxville",
                    CountryId = 2,
                    Rating = 4.5
                });
        }
    }
}
