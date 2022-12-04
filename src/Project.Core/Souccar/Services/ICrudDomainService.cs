using Abp.Domain.Entities;
using Abp.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Souccar.Services
{
    public interface ICrudDomainService<TEntity> : IDomainService where TEntity : class, IEntity<int>
    {
        Task<IList<TEntity>> GetAllAsync();
        IList<TEntity> GetAllIncluding();
        Task DeleteAsync(int id);
        TEntity GetIncluding(int id);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
