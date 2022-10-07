using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository;

public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
{
    readonly HotelListingDbContext _context;

    public HotelsRepository(HotelListingDbContext context) : base(context)
    {
        _context = context;
    }

    public new async Task<Hotel> GetAsync(int? id)
    {
        if (id == null)
        {
            return null;
        }

        return await _context.Hotels.Include(q => q.Address).FirstOrDefaultAsync(q => q.Id == id);
    }
}