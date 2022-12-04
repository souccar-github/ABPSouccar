using AutoMapper;
using Project.Employees.Indecies.Dto;

namespace Project.Employees.Indecies.Map
{
   public class NationalityMapProfile : Profile
    {
        public NationalityMapProfile()
        {
            CreateMap<Nationality, NationalityDto>();
            CreateMap<Nationality, ReadNationalityDto>();
            CreateMap<CreateNationalityDto, Nationality>();
            CreateMap<Nationality, CreateNationalityDto>();
            CreateMap<UpdateNationalityDto, Nationality>();
            CreateMap<Nationality, UpdateNationalityDto>();
        }
    }
}

