using AutoMapper;
using Project.Personnel.Entities.Dto;
using Project.Shared.Dto;

namespace Project.Personnel.Entities.Map
{
   public class ChildrenMapProfile : Profile
    {
        public ChildrenMapProfile()
        {
            CreateMap<Children, ChildrenDto>();
            CreateMap<Children, ReadChildrenDto>();
            CreateMap<ReadChildrenDto, Children>();
            CreateMap<CreateChildrenDto, Children>();
            CreateMap<Children, CreateChildrenDto>();
            CreateMap<UpdateChildrenDto, Children>();
            CreateMap<Children, UpdateChildrenDto>();
        }
    }
}

