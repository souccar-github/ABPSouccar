using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Project.Souccar.Application.Dtos;
using Project.Personnel.Indecies.Services;
using Souccar.Services;

namespace Project.Personnel.Indecies.Services
{
    public interface INationalityDomainService : ICrudDomainService<Nationality, SouccarPagedResultRequestDto>
     {
    }
}

