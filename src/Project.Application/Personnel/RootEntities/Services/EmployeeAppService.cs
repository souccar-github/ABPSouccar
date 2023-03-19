using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Personnel.RootEntities.Dto;
using Project.Shared.Dto;
using Project.Souccar.Application.Dtos;
using Souccar.Services;

namespace Project.Personnel.RootEntities.Services
{
    public class EmployeeAppService : SouccarAppService<Employee,EmployeeDto,CreateEmployeeDto,UpdateEmployeeDto,ReadEmployeeDto ,SouccarPagedResultRequestDto>, IEmployeeAppService
    {
        private readonly IEmployeeDomainService _employeeDomainService;
        public EmployeeAppService(IEmployeeDomainService employeeDomainService) : base(employeeDomainService)
        {
            _employeeDomainService = employeeDomainService;
        }

        public async Task<List<ListViewDto>> GetEmployeesLookUp()
        {
        var list = await _employeeDomainService.GetAllAsync();
        var result = new List<ListViewDto>();
        result = ObjectMapper.Map<List<ListViewDto>>(list);
        return result;
        }
    }
}

