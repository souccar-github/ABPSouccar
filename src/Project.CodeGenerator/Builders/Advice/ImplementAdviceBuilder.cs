using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CodeGenerator.Builders.Advice
{
    internal class ImplementAdviceBuilder
    {
        private readonly Type _entityType;
        private StringBuilder builder;

        public ImplementAdviceBuilder(Type entityType)
        {
            _entityType = entityType;
            this.builder = new StringBuilder();
        }

        public string Genetate()
        {
            var entityName = _entityType.Name;
            var paramName = entityName.FirstCharToLowerCase();
            var namespac = _entityType.Namespace;

            builder.AddDefaultNamespaces();
            builder.AppendLine($"using System.Threading.Tasks;");
            builder.AppendLine($"using Castle.DynamicProxy;");
            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Advices");
            builder.AppendLine("{");

            builder.AppendLine($"    public class {entityName}Advice : I{entityName}Advice");
            builder.AppendLine("    {");

            builder.AppendLine("        public async Task AfterDeleteAsync(IInvocation invocation){}");
            builder.AppendLine("        public async Task AfterInsertAsync(IInvocation invocation){}");
            builder.AppendLine("        public async Task AfterReadAsync(IInvocation invocation){}");
            builder.AppendLine("        public async Task AfterUpdateAsync(IInvocation invocation){}");
            builder.AppendLine("        public async Task BeforeDeleteAsync(IInvocation invocation){}");
            builder.AppendLine("        public async Task BeforeInsertAsync(IInvocation invocation){}");
            builder.AppendLine("        public async Task BeforeReadAsync(IInvocation invocation){}");
            builder.AppendLine("        public async Task BeforeUpdateAsync(IInvocation invocation){}");
            builder.AppendLine("        public void BeforeRead(IInvocation invocation){}");
            builder.AppendLine("        public void AfterRead(IInvocation invocation){}");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
