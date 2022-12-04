using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Souccar.Services;

namespace Project.Employees.Indecies.Services
{
    public class NationalityDomainService :CrudDomainService<Nationality>, INationalityDomainService
    {
        private readonly IRepository<Nationality> _nationalityRepository;
        public NationalityDomainService(IRepository<Nationality> nationalityRepository): base(nationalityRepository)
        {
            _nationalityRepository = nationalityRepository;
        }
        public override Nationality GetIncluding(int id)
        {
            return base.GetIncluding(id);
        }

        public override Task<Nationality> GetAsync(int id)
        {
            return base.GetAsync(id);
        }
        public override IList<Nationality> GetAllIncluding()
        {
            return base.GetAllIncluding();
        }

        public override Task<IList<Nationality>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override Task<Nationality> InsertAsync(Nationality entity)
        {
            return base.InsertAsync(entity);
        }
        public override Task<Nationality> UpdateAsync(Nationality entity)
        {
            return base.UpdateAsync(entity);
        }
        public override Task DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}

