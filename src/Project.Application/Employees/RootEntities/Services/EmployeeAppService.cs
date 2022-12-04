using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Employees.RootEntities.Dto;
using Souccar.Services;

namespace Project.Employees.RootEntities.Services
{
    public class EmployeeAppService : SouccarAppService<Employee,EmployeeDto,CreateEmployeeDto,UpdateEmployeeDto>, IEmployeeAppService
    {
        private readonly IEmployeeDomainService _employeeDomainService;
        public EmployeeAppService(IEmployeeDomainService employeeDomainService) : base(employeeDomainService)
        {
            _employeeDomainService = employeeDomainService;
        }
    }
}

