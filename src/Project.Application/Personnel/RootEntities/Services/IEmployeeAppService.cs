using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Project.Personnel.RootEntities.Dto;
using Souccar.Services;
using Project.Shared.Dto;
using Project.Souccar.Application.Dtos;

namespace Project.Personnel.RootEntities.Services
{
    public interface IEmployeeAppService : ISouccarAppService<EmployeeDto,CreateEmployeeDto,UpdateEmployeeDto,ReadEmployeeDto ,SouccarPagedResultRequestDto>
    {
        Task<List<ListViewDto>> GetEmployeesLookUp();
    }
}

