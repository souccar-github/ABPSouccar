using AutoMapper;
using Project.Personnel.RootEntities.Dto;
using Project.Shared.Dto;

namespace Project.Personnel.RootEntities.Map
{
   public class EmployeeMapProfile : Profile
    {
        public EmployeeMapProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, ReadEmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<Employee, CreateEmployeeDto>();
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, UpdateEmployeeDto>();
            CreateMap<Employee, ListViewDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameForDropDown))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}

