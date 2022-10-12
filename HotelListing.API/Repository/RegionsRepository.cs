using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository;

public class RegionsRepository : GenericRepository<Region>, IRegionsRepository
{
    public RegionsRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
    {

    }
}