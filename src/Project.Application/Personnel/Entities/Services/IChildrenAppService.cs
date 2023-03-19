using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Project.Personnel.Entities.Dto;
using Souccar.Services;
using Project.Shared.Dto;
using Project.Souccar.Application.Dtos;

namespace Project.Personnel.Entities.Services
{
    public interface IChildrenAppService : ISouccarAppService<ChildrenDto,CreateChildrenDto,UpdateChildrenDto,ReadChildrenDto ,SouccarPagedResultRequestDto>
    {
        Task<List<ListViewDto>> GetChildrenLookUp();
    }
}

