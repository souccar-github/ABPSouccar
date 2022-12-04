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
    public class CountryDomainService :CrudDomainService<Country>, ICountryDomainService
    {
        private readonly IRepository<Country> _countryRepository;
        public CountryDomainService(IRepository<Country> countryRepository): base(countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public override Country GetIncluding(int id)
        {
            return base.GetIncluding(id);
        }

        public override Task<Country> GetAsync(int id)
        {
            return base.GetAsync(id);
        }
        public override IList<Country> GetAllIncluding()
        {
            return base.GetAllIncluding();
        }

        public override Task<IList<Country>> GetAllAsync()
        {
            return base.GetAllAsync();
        }
        public override Task<Country> InsertAsync(Country entity)
        {
            return base.InsertAsync(entity);
        }
        public override Task<Country> UpdateAsync(Country entity)
        {
            return base.UpdateAsync(entity);
        }
        public override Task DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}

