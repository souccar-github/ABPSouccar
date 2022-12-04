using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Souccar.Services;

namespace Souccar.Shared.Factories
{
    public interface ISouccarAppFactory : ITransientDependency 
    {
        object Create(string typeName);
    }
}
