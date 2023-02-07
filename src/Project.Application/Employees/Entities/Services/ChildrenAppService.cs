using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Employees.Entities.Dto;
using Souccar.Services;

namespace Project.Employees.Entities.Services
{
    public class ChildrenAppService : SouccarAppService<Children,ChildrenDto,CreateChildrenDto,UpdateChildrenDto,ReadChildrenDto>, IChildrenAppService
    {
        private readonly IChildrenDomainService _childrenDomainService;
        public ChildrenAppService(IChildrenDomainService childrenDomainService) : base(childrenDomainService)
        {
            _childrenDomainService = childrenDomainService;
        }
    }
}

