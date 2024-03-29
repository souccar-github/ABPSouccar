﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CodeGenerator
{
    internal class InterfaceAppServiceBuilder
    {
        private readonly Type _entityType;
        private StringBuilder builder;

        public InterfaceAppServiceBuilder(Type entityType)
        {
            _entityType = entityType;
            this.builder = new StringBuilder();
        }

        public string Genetate()
        {
            var entityName = _entityType.Name;
            var namespac = _entityType.Namespace;

            builder.AddDefaultNamespaces();
            builder.AppendLine("using Abp.Application.Services;");
            builder.AppendLine($"using {namespac}.Dto;");
            builder.AppendLine("using Souccar.Services;");
            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Services");
            builder.AppendLine("{");

            builder.AppendLine($"    public interface I{entityName}AppService : ISouccarAppService<{entityName}Dto,Create{entityName}Dto,Update{entityName}Dto>");
            builder.AppendLine( "    {");

            builder.AppendLine("    }");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
