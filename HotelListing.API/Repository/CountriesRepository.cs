using HotelListing.API.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
        }
    }
}
