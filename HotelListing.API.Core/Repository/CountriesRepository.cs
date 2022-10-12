using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository;

public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
{
    readonly HotelListingDbContext _context;
    readonly IMapper _mapper;

    public CountriesRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CountryDO> GetDetails(int id)
    {
        var result = await _context.Countries
            .Include(q => q.HotelList)
            .Include(q => q.Region)
            .ProjectTo<CountryDO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (result == null)
        {
            throw new NotFoundException(nameof(GetDetails), id);
        }

        return result;
    }

    // public new async Task<List<Country>> GetAllAsync()
    // {
    //     return await _context.Countries.Include(q => q.Region).ToListAsync();
    // }
}