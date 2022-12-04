using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Project.Employees.Entities.Services;
using Souccar.Services;

namespace Project.Employees.Entities.Services
{
    public interface IChildrenDomainService : ICrudDomainService<Children>
     {
    }
}

