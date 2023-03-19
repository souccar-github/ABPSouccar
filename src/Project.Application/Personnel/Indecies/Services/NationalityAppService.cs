using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Personnel.Indecies.Dto;
using Project.Shared.Dto;
using Project.Souccar.Application.Dtos;
using Souccar.Services;

namespace Project.Personnel.Indecies.Services
{
    public class NationalityAppService : SouccarAppService<Nationality,NationalityDto,CreateNationalityDto,UpdateNationalityDto,ReadNationalityDto ,SouccarPagedResultRequestDto>, INationalityAppService
    {
        private readonly INationalityDomainService _nationalityDomainService;
        public NationalityAppService(INationalityDomainService nationalityDomainService) : base(nationalityDomainService)
        {
            _nationalityDomainService = nationalityDomainService;
        }

        public async Task<List<ListViewDto>> GetNationalitiesLookUp()
        {
        var list = await _nationalityDomainService.GetAllAsync();
        var result = new List<ListViewDto>();
        result = ObjectMapper.Map<List<ListViewDto>>(list);
        return result;
        }
    }
}

