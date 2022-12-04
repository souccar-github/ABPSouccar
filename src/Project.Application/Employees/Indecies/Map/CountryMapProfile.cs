using AutoMapper;
using Project.Employees.Indecies.Dto;

namespace Project.Employees.Indecies.Map
{
   public class CountryMapProfile : Profile
    {
        public CountryMapProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<Country, ReadCountryDto>();
            CreateMap<CreateCountryDto, Country>();
            CreateMap<Country, CreateCountryDto>();
            CreateMap<UpdateCountryDto, Country>();
            CreateMap<Country, UpdateCountryDto>();
        }
    }
}

