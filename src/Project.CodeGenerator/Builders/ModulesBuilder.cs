using System;
using System.Linq;
using System.Reflection;

namespace Project.CodeGenerator
{
    internal class ModulesBuilder
    {

        public static void Generate(Assembly assembly, List<string> modules)
        {
            foreach (var moduleName in modules)
            {
                var entit = assembly.GetTypes().ToList();
                var entities = assembly.GetTypes()
                .Where(t => t.Namespace != null
                && t.Namespace.Contains($"{GeneralSetting.ProjectName}.{moduleName}")
                && t.BaseType != null
                && (t.BaseType.Name.Contains("SouccarAggregate")|| t.BaseType.Name.Contains( "SouccarIndex" ) || t.BaseType.Name.Contains("SouccarEntity"))
                && t.IsClass == true).ToList();

                if (entities.Any())
                {
                    foreach (var entity in entities)
                    {
                        ApplicationBuilder.Genetate(entity);
                        DomainBuilder.Genetate(entity);
                    }
                }
            }
        }
    }
}
