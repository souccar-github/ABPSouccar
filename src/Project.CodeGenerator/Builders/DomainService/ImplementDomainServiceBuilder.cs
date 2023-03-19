using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CodeGenerator
{
    internal class ImplementDomainServiceBuilder
    {
        private readonly Type _entityType;
        private StringBuilder builder;

        public ImplementDomainServiceBuilder(Type entityType)
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
            builder.AppendLine("using System.Linq;");
            builder.AppendLine($"using Abp.Domain.Repositories;");
            builder.AppendLine("using System.Collections.Generic;");
            builder.AppendLine($"using {GeneralSetting.ProjectName}.Souccar.Application.Dtos;");
            builder.AppendLine("using System.Threading.Tasks;");
            builder.AppendLine("using Souccar.Services;");
            

            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Services");
            builder.AppendLine("{");

            builder.AppendLine($"    public class {entityName}DomainService :CrudDomainService<{entityName}, SouccarPagedResultRequestDto>, I{entityName}DomainService");
            builder.AppendLine( "    {");

            builder.AppendLine($"        private readonly IRepository<{entityName}> _{paramName}Repository;");
            builder.AppendLine($"        public {entityName}DomainService(IRepository<{entityName}> {paramName}Repository): base({paramName}Repository)");
            builder.AppendLine( "        {");
            builder.AppendLine($"            _{paramName}Repository = {paramName}Repository;");
            builder.AppendLine( "        }");

            GenerateGetQueryable(builder, entityName);
            GenerateGetAllAsync(builder, entityName);
            GenerateCreateAsync(builder, entityName);
            GenerateUpdateAsync(builder, entityName);
            GenerateDeleteAsync(builder, entityName);

            builder.AppendLine( "    }");
            builder.AppendLine("}");

            return builder.ToString();
        }

        private void GenerateDeleteAsync(StringBuilder builder, string entityName)
        {
            builder.AppendLine($"        public override Task DeleteAsync(int id)");
            builder.AppendLine("        {");
            builder.AppendLine($"            return base.DeleteAsync(id);");
            builder.AppendLine("        }");
        }

        private void GenerateUpdateAsync(StringBuilder builder, string entityName)
        {
            builder.AppendLine($"        public override Task<{entityName}> UpdateAsync({entityName} entity)");
            builder.AppendLine("        {");
            builder.AppendLine($"            return base.UpdateAsync(entity);");
            builder.AppendLine("        }");
        }

        private void GenerateCreateAsync(StringBuilder builder, string entityName)
        {
            builder.AppendLine($"        public override Task<{entityName}> InsertAsync({entityName} entity)");
            builder.AppendLine("        {");
            builder.AppendLine($"            return base.InsertAsync(entity);");
            builder.AppendLine("        }");
        }

        private void GenerateGetQueryable(StringBuilder builder, string entityName)
        {
            builder.AppendLine($"        public override {entityName} GetIncluding(int id)");
            builder.AppendLine( "        {");
            builder.AppendLine($"            return base.GetIncluding(id);");
            builder.AppendLine( "        }");
            builder.AppendLine();
            builder.AppendLine($"        public override Task<{entityName}> GetAsync(int id)");
            builder.AppendLine("        {");
            builder.AppendLine($"            return base.GetAsync(id);");
            builder.AppendLine("        }");
        }

        private void GenerateGetAllAsync(StringBuilder builder,string entityName)
        {
            builder.AppendLine($"        public override IList<{entityName}> GetAllIncluding()");
            builder.AppendLine( "        {");
            builder.AppendLine($"            return base.GetAllIncluding();");
            builder.AppendLine( "        }");
            builder.AppendLine();
            builder.AppendLine($"        public override Task<IList<{entityName}>> GetAllAsync()");
            builder.AppendLine("        {");
            builder.AppendLine($"            return base.GetAllAsync();");
            builder.AppendLine("        }");

        }
    }
}
