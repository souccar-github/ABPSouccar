using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Souccar.Services
{
    public interface ISouccarAppService<TEntityDto,TCreateEntityDto,TUpdateEntityDto,TReadEntityDto> : IApplicationService where TEntityDto : class, IEntityDto<int>
                                                                                           where TCreateEntityDto : class, IEntityDto<int>
                                                                                           where TUpdateEntityDto : class, IEntityDto<int>
                                                                                           where TReadEntityDto : class, IEntityDto<int>
    {
        Task<IList<TReadEntityDto>> GetAllAsync();
        Task<IList<TReadEntityDto>> GetAllIncludingAsync();
        Task DeleteAsync(int id);
        Task<TEntityDto> GetAsync(int id);
        Task<TEntityDto> GetIncludingAsync(int id);
        Task<TEntityDto> InsertAsync(TCreateEntityDto entity);
        Task<TEntityDto> UpdateAsync(TUpdateEntityDto entity);
    }

    
}
