using Castle.Windsor;
using Project;
using Souccar.Shared.Instances;
using System;
using System.Linq;
using System.Reflection;

namespace Souccar.Shared.Factories
{
    public class SouccarAppFactory : ISouccarAppFactory 
    {
        private readonly IWindsorContainer _container;
        public SouccarAppFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public object Create(string typeName)
        {
            var serviceType = GetApplicationServiceType(typeName);
            var obj = _container.Resolve(serviceType);
            
            return obj;
        }

        private Type GetApplicationServiceType(string typeName)
        {
            var entityName = typeName.Split('.').Last().Replace("Dto", "") + "AppService";

            var domainAssembly = Assembly.GetAssembly(typeof(ProjectCoreModule));
            var type = AppsServices.Instance().FirstOrDefault(x => x.Name == entityName);
            return type;
        }
    }
}
