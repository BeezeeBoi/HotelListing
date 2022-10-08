using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;
using HotelListing.API.Models.Region;

namespace HotelListing.API.Config;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Country, CreateCountryDO>().ReverseMap();
        CreateMap<Country, GetCountryDO>().ReverseMap();
        CreateMap<Country, CountryDO>().ReverseMap();
        CreateMap<Country, UpdateCountryDO>().ReverseMap();

        CreateMap<Region, RegionDO>().ReverseMap();
        CreateMap<Region, GetRegionDO>().ReverseMap();
        CreateMap<Region, PostRegionDO>().ReverseMap();
        CreateMap<Region, UpdateRegionDO>().ReverseMap();
        CreateMap<Region, IncludeRegionDO>().ReverseMap();

        CreateMap<Hotel, HotelDO>().ReverseMap();
        CreateMap<Hotel, GetHotelDO>().ReverseMap();
        CreateMap<Hotel, UpdateHotelDO>().ReverseMap();
    }
}