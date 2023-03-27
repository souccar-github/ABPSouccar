using Project.Personnel.RootEntities;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Project.CodeGenerator
{
    public class MapBuilder
    {
        private readonly Type _entityType;
        private StringBuilder builder;
        public MapBuilder(Type entityType)
        {
            _entityType = entityType;
            builder = new StringBuilder();
        }
        public string Genetate()
        {
            
            var className = _entityType.Name;
            var namespac = _entityType.Namespace;

            builder.AppendLine("using AutoMapper;");
            builder.AppendLine($"using {namespac}.Dto;");
            builder.AppendLine($"using {GeneralSetting.ProjectName}.Shared.Dto;");
            builder.AppendLine("");

            //namespace
            builder.AppendLine($"namespace {namespac}.Map");
            builder.AppendLine("{");

            //class
            GenerateClass();

            builder.AppendLine("}");

            return builder.ToString();
        }

        private void GenerateClass()
        {
            var name = $"{_entityType.Name}";

            builder.AppendLine($"   public class {name}MapProfile : Profile");
            builder.AppendLine("    {");
            builder.AppendLine($"        public {name}MapProfile()");
            builder.AppendLine("        {");

            builder.AppendLine($"            CreateMap<{name}, {name}Dto>();");
            builder.AppendLine($"            CreateMap<{name}, Read{name}Dto>();");
            builder.AppendLine($"            CreateMap<Read{name}Dto, {name}>();");
            builder.AppendLine($"            CreateMap<Create{name}Dto, {name}>();");
            builder.AppendLine($"            CreateMap<{name}, Create{name}Dto>();");
            builder.AppendLine($"            CreateMap<Update{name}Dto, {name}>();");
            builder.AppendLine($"            CreateMap<{name}, Update{name}Dto>();");
            foreach(var propInfo in _entityType.GetProperties())
            {
                if (propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute") != null && propInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "SouccarUIPAttribute").NamedArguments.Select(x => x.MemberName).Contains("ForDropDown"))
                {
                    builder.AppendLine($"            CreateMap<{name}, ListViewDto>()");
                    builder.AppendLine($"            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.{propInfo.Name}))");
                    builder.AppendLine($"            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));");
                }
            }
            builder.AppendLine("        }");
            builder.AppendLine("    }");
        }

        
    }
}
