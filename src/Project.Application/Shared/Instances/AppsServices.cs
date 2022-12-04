using Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Souccar.Shared.Instances
{
    public class AppsServices
    {
        private static IList<Type> _serviceType;
        private static readonly object obj = new object();
        private AppsServices()
        {

        }

        public static IList<Type> Instance()
        {
            lock (obj)
            {
                if (_serviceType == null)
                {
                    _serviceType = new List<Type>();
                    _serviceType = GetServicesTypes();
                }
            }

            return _serviceType;
        }

        private static IList<Type> GetServicesTypes()
        {
            var serviceAssembly = Assembly.GetAssembly(typeof(ProjectApplicationModule));
            return serviceAssembly.GetTypes().ToList();

        }
    }
}
