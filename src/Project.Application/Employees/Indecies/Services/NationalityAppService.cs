using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Employees.Indecies.Dto;
using Souccar.Services;

namespace Project.Employees.Indecies.Services
{
    public class NationalityAppService : SouccarAppService<Nationality,NationalityDto,CreateNationalityDto,UpdateNationalityDto,ReadNationalityDto>, INationalityAppService
    {
        private readonly INationalityDomainService _nationalityDomainService;
        public NationalityAppService(INationalityDomainService nationalityDomainService) : base(nationalityDomainService)
        {
            _nationalityDomainService = nationalityDomainService;
        }
    }
}

