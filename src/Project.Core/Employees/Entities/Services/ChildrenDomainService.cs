using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Souccar.Services;

namespace Project.Employees.Entities.Services
{
    public class ChildrenDomainService :CrudDomainService<Children>, IChildrenDomainService
    {
        private readonly IRepository<Children> _childrenRepository;
        public ChildrenDomainService(IRepository<Children> childrenRepository): base(childrenRepository)
        {
            _childrenRepository = childrenRepository;
        }
        public override Children GetIncluding(int id)
        {
            return base.GetIncluding(id);
        }

        public override Task<Children> GetAsync(int id)
        {
            return base.GetAsync(id);
        }
        public override IList<Children> GetAllIncluding()
        {
            return base.GetAllIncluding();
        }

        public override Task<IList<Children>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override Task<Children> InsertAsync(Children entity)
        {
            return base.InsertAsync(entity);
        }
        public override Task<Children> UpdateAsync(Children entity)
        {
            return base.UpdateAsync(entity);
        }
        public override Task DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}

