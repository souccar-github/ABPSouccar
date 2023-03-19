using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Personnel.Entities.Dto;
using Project.Shared.Dto;
using Project.Souccar.Application.Dtos;
using Souccar.Services;

namespace Project.Personnel.Entities.Services
{
    public class ChildrenAppService : SouccarAppService<Children,ChildrenDto,CreateChildrenDto,UpdateChildrenDto,ReadChildrenDto ,SouccarPagedResultRequestDto>, IChildrenAppService
    {
        private readonly IChildrenDomainService _childrenDomainService;
        public ChildrenAppService(IChildrenDomainService childrenDomainService) : base(childrenDomainService)
        {
            _childrenDomainService = childrenDomainService;
        }

        public async Task<List<ListViewDto>> GetChildrenLookUp()
        {
        var list = await _childrenDomainService.GetAllAsync();
        var result = new List<ListViewDto>();
        result = ObjectMapper.Map<List<ListViewDto>>(list);
        return result;
        }
    }
}

