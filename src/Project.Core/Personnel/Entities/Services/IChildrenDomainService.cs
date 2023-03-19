using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Project.Souccar.Application.Dtos;
using Project.Personnel.Entities.Services;
using Souccar.Services;

namespace Project.Personnel.Entities.Services
{
    public interface IChildrenDomainService : ICrudDomainService<Children, SouccarPagedResultRequestDto>
     {
    }
}

