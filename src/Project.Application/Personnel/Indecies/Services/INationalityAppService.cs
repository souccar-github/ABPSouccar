using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Project.Personnel.Indecies.Dto;
using Souccar.Services;
using Project.Shared.Dto;
using Project.Souccar.Application.Dtos;

namespace Project.Personnel.Indecies.Services
{
    public interface INationalityAppService : ISouccarAppService<NationalityDto,CreateNationalityDto,UpdateNationalityDto,ReadNationalityDto ,SouccarPagedResultRequestDto>
    {
        Task<List<ListViewDto>> GetNationalitiesLookUp();
    }
}

