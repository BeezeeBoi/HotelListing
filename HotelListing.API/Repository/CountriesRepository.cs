using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        readonly HotelListingDbContext _context;

        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(q => q.HotelList).Include(q => q.Region).FirstOrDefaultAsync(q => q.Id == id);
        }

        public new async Task<List<Country>> GetAllAsync()
        {
            return await _context.Countries.Include(q => q.Region).ToListAsync();
        }
    }
}
