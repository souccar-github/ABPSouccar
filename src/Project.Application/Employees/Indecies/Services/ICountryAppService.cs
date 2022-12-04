using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Project.Employees.Indecies.Dto;
using Souccar.Services;

namespace Project.Employees.Indecies.Services
{
    public interface ICountryAppService : ISouccarAppService<CountryDto,CreateCountryDto,UpdateCountryDto>
    {
    }
}

