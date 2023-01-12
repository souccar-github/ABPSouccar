using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Threading;
using Project;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Souccar.Services
{
    public class SouccarAppService<TEntity, TEntityDto,TCreateEntityDto,TUpdateEntityDto,TReadEntityDto> : ProjectAppServiceBase, ISouccarAppService<TEntityDto, TCreateEntityDto, TUpdateEntityDto, TReadEntityDto> 
        where TEntity : class, IEntity<int> 
        where TEntityDto : class, IEntityDto<int>
        where TCreateEntityDto : class, IEntityDto<int>
        where TUpdateEntityDto : class, IEntityDto<int>
        where TReadEntityDto : class, IEntityDto<int>
    {
        private readonly ICrudDomainService<TEntity> _domainService;
        public SouccarAppService(ICrudDomainService<TEntity> domainService)
        {
            _domainService = domainService;
        }

        public virtual async Task<IList<TReadEntityDto>> GetAllIncludingAsync()
        {
            var data = _domainService.GetAllIncluding();
            var list = ObjectMapper.Map<List<TReadEntityDto>>(data.ToList());
            return list;
        }
        public virtual async Task<IList<TReadEntityDto>> GetAllAsync()
        {
            var data = await _domainService.GetAllAsync();
            return ObjectMapper.Map<IList<TReadEntityDto>>(data);
        }


        public virtual async Task DeleteAsync(int id)
        {
            await _domainService.DeleteAsync(id);
        }

        public virtual async Task<TEntityDto> GetIncludingAsync(int id)
        {
            var data =_domainService.GetIncluding(id);
            return ObjectMapper.Map<TEntityDto>(data);
        }

        public virtual async Task<TEntityDto> GetAsync(int id)
        {
            var data =await _domainService.GetAsync(id);
            return ObjectMapper.Map<TEntityDto>(data);
        }

        public virtual async Task<TEntityDto> InsertAsync(TCreateEntityDto entity)
        {
            var param = ObjectMapper.Map<TEntity>(entity);
            var data = await _domainService.InsertAsync(param);
            return ObjectMapper.Map<TEntityDto>(data);
        }
        public async virtual Task<TEntityDto> UpdateAsync(TUpdateEntityDto entity)
        {
            var param = ObjectMapper.Map<TEntity>(entity);
            var data = await _domainService.UpdateAsync(param);
            return ObjectMapper.Map<TEntityDto>(data);
        }
    }
}
