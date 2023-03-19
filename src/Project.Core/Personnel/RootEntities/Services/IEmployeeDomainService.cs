using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Project.Souccar.Application.Dtos;
using Project.Personnel.RootEntities.Services;
using Souccar.Services;

namespace Project.Personnel.RootEntities.Services
{
    public interface IEmployeeDomainService : ICrudDomainService<Employee, SouccarPagedResultRequestDto>
     {
    }
}

