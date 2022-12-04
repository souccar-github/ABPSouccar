using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Project.Employees.Indecies.Services;
using Souccar.Services;

namespace Project.Employees.Indecies.Services
{
    public interface INationalityDomainService : ICrudDomainService<Nationality>
     {
    }
}

