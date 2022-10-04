using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Country, CreateCountryDO>().ReverseMap();
            CreateMap<Country, GetCountryDO>().ReverseMap();
            CreateMap<Country, CountryDO>().ReverseMap();
            CreateMap<Country, UpdateCountryDO>().ReverseMap();
            CreateMap<Hotel, HotelDO>().ReverseMap();
        }
    }
}
