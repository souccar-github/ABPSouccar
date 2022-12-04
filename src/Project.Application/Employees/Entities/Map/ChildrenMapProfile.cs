using AutoMapper;
using Project.Employees.Entities.Dto;

namespace Project.Employees.Entities.Map
{
   public class ChildrenMapProfile : Profile
    {
        public ChildrenMapProfile()
        {
            CreateMap<Children, ChildrenDto>();
            CreateMap<Children, ReadChildrenDto>();
            CreateMap<CreateChildrenDto, Children>();
            CreateMap<Children, CreateChildrenDto>();
            CreateMap<UpdateChildrenDto, Children>();
            CreateMap<Children, UpdateChildrenDto>();
        }
    }
}

