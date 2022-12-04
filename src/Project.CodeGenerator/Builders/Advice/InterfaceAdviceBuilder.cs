using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CodeGenerator.Builders.Advice
{
    internal class InterfaceAdviceBuilder
    {
        private readonly Type _entityType;
        private StringBuilder builder;

        public InterfaceAdviceBuilder(Type entityType)
        {
            _entityType = entityType;
            this.builder = new StringBuilder();
        }

        public string Genetate()
        {
            var entityName = _entityType.Name;
            var namespac = _entityType.Namespace;

            builder.AddDefaultNamespaces();
            builder.AppendLine("using Project.Shared.Advice;");
            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Advices");
            builder.AppendLine("{");

            builder.AppendLine($"    public interface I{entityName}Advice : ISouccarAdvice<{entityName}>");
            builder.AppendLine("     {");

            builder.AppendLine("     }");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
