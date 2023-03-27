using AutoMapper;
using Project.Personnel.Indecies.Dto;
using Project.Shared.Dto;

namespace Project.Personnel.Indecies.Map
{
   public class NationalityMapProfile : Profile
    {
        public NationalityMapProfile()
        {
            CreateMap<Nationality, NationalityDto>();
            CreateMap<Nationality, ReadNationalityDto>();
            CreateMap<ReadNationalityDto, Nationality>();
            CreateMap<CreateNationalityDto, Nationality>();
            CreateMap<Nationality, CreateNationalityDto>();
            CreateMap<UpdateNationalityDto, Nationality>();
            CreateMap<Nationality, UpdateNationalityDto>();
            CreateMap<Nationality, ListViewDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}

