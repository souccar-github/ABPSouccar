using PluralizeService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CodeGenerator
{
    internal class ImplementAppServiceBuilder
    {
        private readonly Type _entityType;
        private StringBuilder builder;

        public ImplementAppServiceBuilder(Type entityType)
        {
            _entityType = entityType;
            this.builder = new StringBuilder();
        }

        public string Genetate()
        {
            var entityName = _entityType.Name;
            var paramName = entityName.FirstCharToLowerCase();
            var idDataType = GeneralSetting.DataTypeId;
            var namespac = _entityType.Namespace;

            builder.AddDefaultNamespaces();
            builder.AppendLine($"using {namespac}.Dto;");
            builder.AppendLine($"using {GeneralSetting.ProjectName}.Shared.Dto;");
            builder.AppendLine($"using {GeneralSetting.ProjectName}.Souccar.Application.Dtos;");
            builder.AppendLine("using Souccar.Services;");
            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Services");
            builder.AppendLine("{");

            builder.AppendLine($"    public class {entityName}AppService : {GeneralSetting.AppServiceBase}<{entityName},{entityName}Dto,Create{entityName}Dto,Update{entityName}Dto,Read{entityName}Dto ,SouccarPagedResultRequestDto>, I{entityName}AppService");
            builder.AppendLine("    {");

            builder.AppendLine($"        private readonly I{entityName}DomainService _{paramName}DomainService;");
            builder.AppendLine($"        public {entityName}AppService(I{entityName}DomainService {paramName}DomainService) : base({paramName}DomainService)");
            builder.AppendLine("        {");
            builder.AppendLine($"            _{paramName}DomainService = {paramName}DomainService;");
            builder.AppendLine("        }");
            builder.AppendLine("");
            builder.AppendLine($"        public async Task<List<ListViewDto>> Get{PluralizationProvider.Pluralize(entityName)}LookUp()");
            builder.AppendLine("        {");
            builder.AppendLine($"        var list = await _{paramName}DomainService.GetAllAsync();");
            builder.AppendLine($"        var result = new List<ListViewDto>();");
            builder.AppendLine("        result = ObjectMapper.Map<List<ListViewDto>>(list);");
            builder.AppendLine("        return result;");
            builder.AppendLine("        }");

            builder.AppendLine("    }");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}