using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Project.CodeGenerator
{
    public class DtoBuilder
    {
        private readonly Type _entityType;
        private readonly string _prefix;
        private StringBuilder builder;
        public DtoBuilder(Type entityType, string prefix = "")
        {
            _entityType = entityType;
            _prefix = prefix;
            builder = new StringBuilder();
        }
        public string Genetate()
        {
            var className = _entityType.Name;
            var namespac = _entityType.Namespace;

            builder.AppendLine("using System;");
            builder.AppendLine("using Abp.Application.Services.Dto;");
            builder.AppendLine("using System.Collections.Generic;");
            GenerateUsings();
            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Dto");
            builder.AppendLine("{");

            //class
            GenerateClass();

            builder.AppendLine("}");

            return builder.ToString();
        }
        private void GenerateUsings()
        {
            var refProperties = _entityType.GetProperties()
                .Where(x => x.PropertyType.BaseType != null
                    && x.PropertyType.BaseType.BaseType != null
                    && x.PropertyType.BaseType.BaseType.FullName.Contains("Entities")).ToList();
            var listProperties = _entityType.GetProperties()
                .Where(x => x.PropertyType != null
                    && x.PropertyType.FullName.Contains("List"))
                .ToList();
            var enumProperties = _entityType.GetProperties()
                .Where(x => x.PropertyType != null
                    && x.PropertyType.FullName.Contains("Enums"))
                .ToList();
            foreach (var propInfo in refProperties)
            {
                var _namespace = propInfo.PropertyType.Namespace;
                if (!builder.ToString().Contains(_namespace))
                {
                    builder.AppendLine($"using {_namespace}.Dto;");
                }
            }

            foreach (var propInfo in listProperties)
            {
                var _namespace = propInfo.PropertyType.GetProperties().FirstOrDefault().PropertyType.Namespace;
                if (!builder.ToString().Contains(_namespace))
                {
                    builder.AppendLine($"using {_namespace}.Dto;");
                }
            }
            foreach (var propInfo in enumProperties)
            {
                var _namespace = propInfo.PropertyType.GenericTypeArguments[0].Namespace;
                if (!builder.ToString().Contains(_namespace))
                {
                    builder.AppendLine($"using {_namespace};");
                }
            }
        }
            private void GenerateClass()
        {
            builder.AppendLine($"   public class {_prefix}{_entityType.Name}Dto : EntityDto<{GeneralSetting.DataTypeId}>");
            builder.AppendLine("    {");

            #region properties
            if (_entityType.BaseType.Name.Contains("SouccarIndex"))
            {
                builder.AppendLine("        public string Name {get; set;}");
                builder.AppendLine("        public int Order {get; set;}");
            }

            var properties = _entityType.GetProperties(BindingFlags.Public
                                                        | BindingFlags.Instance
                                                        | BindingFlags.DeclaredOnly);
            var refProperties = _entityType.GetProperties()
                .Where(x => x.PropertyType.BaseType != null
                    && x.PropertyType.BaseType.BaseType != null
                    && x.PropertyType.BaseType.BaseType.FullName.Contains("Entities")).ToList();
            var enumProperties = _entityType.GetProperties()
                .Where(x => x.PropertyType != null
                    && x.PropertyType.FullName.Contains("Enums"))
                .ToList();
            var listProperties = _entityType.GetProperties()
                .Where(x => x.PropertyType != null
                    && x.PropertyType.FullName.Contains("List"))
                .ToList();


            
            foreach (var propInfo in properties)
            {
                if (_prefix == "Read")
                {
                    if (propInfo.CustomAttributes.Count() > 0 && propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.Count() > 0)
                    {
                        var attribute = propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.FirstOrDefault(x => x.MemberName == "ForGridView");
                        if ((Boolean)attribute.TypedValue.Value == true)
                        {
                            GenerateProperty(propInfo);
                        }
                    }

                }
                else
                {
                    GenerateProperty(propInfo);
                }
            }

            foreach (var propInfo in refProperties)
            {
                if (_prefix == "Read")
                {
                    if (propInfo.CustomAttributes.Count() > 0 && propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.Count() > 0)
                    {
                        var attribute = propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.FirstOrDefault(x => x.MemberName == "ForGridView");
                        if ((Boolean)attribute.TypedValue.Value == true)
                        {
                            GenerateRefProperty(propInfo);
                        }
                    }
                      
                }
                else
                {
                    GenerateRefProperty(propInfo);
                }
            }

            foreach (var propInfo in listProperties)
            {
                if (_prefix == "Read")
                {
                    if (propInfo.CustomAttributes.Count() > 0 && propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.Count() > 0)
                    {
                        var attribute = propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.FirstOrDefault(x => x.MemberName == "ForGridView");
                        if ((Boolean)attribute.TypedValue.Value == true)
                        {
                            GenerateListProperty(propInfo);
                        }
                    }

                }
                else
                {
                    GenerateListProperty(propInfo);
                }
            }

            foreach (var propInfo in enumProperties)
            {
                if (_prefix == "Read")
                {
                    if (propInfo.CustomAttributes.Count() > 0 && propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.Count() > 0)
                    {
                        var attribute = propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.FirstOrDefault(x => x.MemberName == "ForGridView");
                        if ((Boolean)attribute.TypedValue.Value == true)
                        {
                            GenerateEnumProperty(propInfo);
                        }
                    }

                }
                else
                {
                    GenerateEnumProperty(propInfo);
                }
            }

            #endregion

            builder.AppendLine("    }");
        }

        private void GenerateProperty(PropertyInfo propInfo)
        {
            var propText = propInfo.GetText();
            if (!string.IsNullOrEmpty(propText))
            {
                builder.AppendLine($"        {propText}");
            }
        }

        private void GenerateRefProperty(PropertyInfo propInfo)
        {
            var propText = propInfo.GetRefText(_prefix);
            builder.AppendLine($"        {propText}");
        }

        private void GenerateListProperty(PropertyInfo propInfo)
        {
            var propText = propInfo.GetListText(_prefix);
            builder.AppendLine($"        {propText}");
        }

        private void GenerateEnumProperty(PropertyInfo propInfo)
        {
            var propText = propInfo.GetEnumText();
            builder.AppendLine($"        {propText}");
        }
    }
}
