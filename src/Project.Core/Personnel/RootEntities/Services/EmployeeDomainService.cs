using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using Project.Souccar.Application.Dtos;
using System.Threading.Tasks;
using Souccar.Services;

namespace Project.Personnel.RootEntities.Services
{
    public class EmployeeDomainService :CrudDomainService<Employee, SouccarPagedResultRequestDto>, IEmployeeDomainService
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeDomainService(IRepository<Employee> employeeRepository): base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public override Employee GetIncluding(int id)
        {
            return base.GetIncluding(id);
        }

        public override Task<Employee> GetAsync(int id)
        {
            return base.GetAsync(id);
        }
        public override IList<Employee> GetAllIncluding()
        {
            return base.GetAllIncluding();
        }

        public override Task<IList<Employee>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override Task<Employee> InsertAsync(Employee entity)
        {
            return base.InsertAsync(entity);
        }
        public override Task<Employee> UpdateAsync(Employee entity)
        {
            return base.UpdateAsync(entity);
        }
        public override Task DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}

