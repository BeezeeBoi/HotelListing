using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Core.Repository;

public class RegionsRepository : GenericRepository<Region>, IRegionsRepository
{
    public RegionsRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
    {

    }
}