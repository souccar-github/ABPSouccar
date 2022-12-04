using Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Souccar.Shared.Instances
{
    public class DomainEntities
    {
        private static IList<Type> _domainType;
        private static readonly object obj = new object();
        private DomainEntities()
        {
            
        }

        public static IList<Type> Instance()
        {
            if (_domainType == null)
            {
                _domainType = new List<Type>();
                _domainType = GetEntitiesTypes();
            }

            return _domainType;
        }

        private static IList<Type> GetEntitiesTypes()
        {
            var domainAssembly = Assembly.GetAssembly(typeof(ProjectCoreModule));
            return domainAssembly.GetTypes().ToList();
            
        }
    }
}
