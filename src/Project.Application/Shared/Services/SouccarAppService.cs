using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Linq;
using Abp.Threading;
using Project;
using Project.Souccar.Application.Dtos;
using Project.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Souccar.Services
{
    public class SouccarAppService<TEntity, TEntityDto,TCreateEntityDto,TUpdateEntityDto,TReadEntityDto, TGetAllInput> : ProjectAppServiceBase, ISouccarAppService<TEntityDto, TCreateEntityDto, TUpdateEntityDto, TReadEntityDto, TGetAllInput> 
        where TEntity : class, IEntity<int> 
        where TEntityDto : class, IEntityDto<int>
        where TCreateEntityDto : class, IEntityDto<int>
        where TUpdateEntityDto : class, IEntityDto<int>
        where TReadEntityDto : class, IEntityDto<int>
        where TGetAllInput : SouccarPagedResultRequestDto
    {
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        private readonly ICrudDomainService<TEntity, TGetAllInput> _domainService;
        public SouccarAppService(ICrudDomainService<TEntity, TGetAllInput> domainService)
        {
            _domainService = domainService;
        }

        public virtual async Task<IList<TReadEntityDto>> GetAllIncludingAsync()
        {
            var data = _domainService.GetAllIncluding();
            var list = ObjectMapper.Map<List<TReadEntityDto>>(data.ToList());
            return list;
        }
        public virtual async Task<PagedResultDto<TReadEntityDto>> GetAllIncludingWithInputAsync(TGetAllInput input)
        {
            IQueryable<TReadEntityDto> query;
            if (input.Keyword == null)
            {
                var _list = await GetAllIncludingAsync();
                query = _list.AsQueryable();
            }
            else
            {
                var _query = _domainService.CreateFilteredIncludingQuery(input);
                query = MapToReadEntityDto(_query.ToList()).AsQueryable();
            }
            int totalCount = query.ToList().Count();
            IList<TEntity> entityDto = MapToEntityDto(query.ToList());
            var applyPaging = _domainService.ApplyPaging(entityDto.AsQueryable(), input);
            query = MapToReadEntityDto(applyPaging.ToList()).AsQueryable();
            var list = new PagedResultDto<TReadEntityDto>(totalCount, MapToEntityDto(query.ToList()).ToList().Select(new Func<TEntity, TReadEntityDto>(MapToReadEntityDto)).ToList());
            if (input.OrderBy != null)
            {
                var parameter = Expression.Parameter(typeof(TReadEntityDto), "x");
                var member = Expression.Property(parameter, input.OrderBy);
                var finalExpression = Expression.Lambda<Func<TReadEntityDto, object>>(member, parameter);
                var queryableList = list.Items.AsQueryable();
                list.Items = queryableList.OrderBy(finalExpression).ToList();
            }
            return list;
        }
        public virtual async Task<IList<TReadEntityDto>> GetAllAsync()
        {
            var data = await _domainService.GetAllAsync();
            return ObjectMapper.Map<IList<TReadEntityDto>>(data);
        }

        public virtual async Task<PagedResultDto<TReadEntityDto>> GetAllWithInputAsync(TGetAllInput input)
        {
            IQueryable<TReadEntityDto> query;
            if (input.Keyword == null)
            {
                var _list = await GetAllAsync();
                query = _list.AsQueryable();
            }
            else
            {
                var _query = await _domainService.CreateFilteredQuery(input);
                query = MapToReadEntityDto(_query.ToList()).AsQueryable();
            }
            int totalCount = query.ToList().Count() ;
            IList<TEntity> entityDto = MapToEntityDto(query.ToList());
            var applyPaging = _domainService.ApplyPaging(entityDto.AsQueryable(), input);
            query = MapToReadEntityDto(applyPaging.ToList()).AsQueryable();
            var list = new PagedResultDto<TReadEntityDto>(totalCount, MapToEntityDto(query.ToList()).ToList().Select(new Func<TEntity, TReadEntityDto>(MapToReadEntityDto)).ToList());
            if (input.OrderBy != null)
            {
                var parameter = Expression.Parameter(typeof(TReadEntityDto), "x");
                var member = Expression.Property(parameter, input.OrderBy);
                var finalExpression = Expression.Lambda<Func<TReadEntityDto, object>>(member, parameter);
                var queryableList = list.Items.AsQueryable();
                list.Items = queryableList.OrderBy(finalExpression).ToList();
            }
            return list;
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

        protected virtual TReadEntityDto MapToReadEntityDto(TEntity entity)
        {
            return base.ObjectMapper.Map<TReadEntityDto>(entity);
        }

        protected virtual IList<TReadEntityDto> MapToReadEntityDto(IList< TEntity> entities)
        {
            return base.ObjectMapper.Map<IList<TReadEntityDto>>(entities);
        }

        protected virtual IList<TEntity> MapToEntityDto(IList<TReadEntityDto > entities)
        {
            return base.ObjectMapper.Map<IList<TEntity>>(entities);
        }

    }
}
