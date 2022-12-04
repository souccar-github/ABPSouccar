using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Project.Employees.RootEntities.Dto;
using Souccar.Services;

namespace Project.Employees.RootEntities.Services
{
    public interface IEmployeeAppService : ISouccarAppService<EmployeeDto,CreateEmployeeDto,UpdateEmployeeDto>
    {
    }
}

