using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Project.Employees.Entities.Dto;
using Souccar.Services;

namespace Project.Employees.Entities.Services
{
    public interface IChildrenAppService : ISouccarAppService<ChildrenDto,CreateChildrenDto,UpdateChildrenDto,ReadChildrenDto>
    {
    }
}

