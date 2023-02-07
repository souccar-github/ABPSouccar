using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Employees.Indecies.Dto;
using Souccar.Services;

namespace Project.Employees.Indecies.Services
{
    public class CountryAppService : SouccarAppService<Country,CountryDto,CreateCountryDto,UpdateCountryDto,ReadCountryDto>, ICountryAppService
    {
        private readonly ICountryDomainService _countryDomainService;
        public CountryAppService(ICountryDomainService countryDomainService) : base(countryDomainService)
        {
            _countryDomainService = countryDomainService;
        }
    }
}

