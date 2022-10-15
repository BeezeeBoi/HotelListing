using AutoMapper;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Core.Models.Region;
using HotelListing.API.Core.Models.Users;
using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Core.Config;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Country, PostCountryDO>().ReverseMap();
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

        CreateMap<User, UserDO>().ReverseMap();
        CreateMap<User, AuthResponseDO>().ReverseMap();
        CreateMap<User, UserLoginDO>().ReverseMap();
    }
}