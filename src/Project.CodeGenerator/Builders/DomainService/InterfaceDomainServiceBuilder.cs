using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CodeGenerator
{
    internal class InterfaceDomainServiceBuilder
    {
        private readonly Type _entityType;
        private StringBuilder builder;

        public InterfaceDomainServiceBuilder(Type entityType)
        {
            _entityType = entityType;
            this.builder = new StringBuilder();
        }

        public string Genetate()
        {
            var entityName = _entityType.Name;
            var namespac = _entityType.Namespace;

            builder.AddDefaultNamespaces();
            builder.AppendLine("using Abp.Domain.Services;");
            builder.AppendLine($"using {GeneralSetting.ProjectName}.Souccar.Application.Dtos;");
            builder.AppendLine($"using {namespac}.Services;");
            builder.AppendLine($"using Souccar.Services;");
            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Services");
            builder.AppendLine("{");

            builder.AppendLine($"    public interface I{entityName}DomainService : ICrudDomainService<{entityName}, SouccarPagedResultRequestDto>");
            builder.AppendLine("     {");

            builder.AppendLine("    }");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
