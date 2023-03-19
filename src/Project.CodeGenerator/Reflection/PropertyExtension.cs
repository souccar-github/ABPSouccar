using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.CodeGenerator
{
    public static  class PropertyExtension
    {
        public static string GetRefText(this PropertyInfo propertyInfo,string _prefix)
        {
            var propType = propertyInfo.PropertyType.Name;
            var name = propertyInfo.Name;
            return $"public {_prefix}{propType}Dto {name} " + "{ get; set; }";
        }
        public static string GetListText(this PropertyInfo propertyInfo,string _prefix)
        {
            var propType = propertyInfo.PropertyType.GenericTypeArguments.FirstOrDefault().Name;
            var name = propertyInfo.Name;

            return $"public List<{_prefix}{propType}Dto> {name} " + "{ get; set; }";
        }
        public static string GetEnumText(this PropertyInfo propertyInfo)
        {
            var name = propertyInfo.Name;
            var propType = propertyInfo.PropertyType.Name;

            return $"public {propType} {name} " + "{ get; set; }";
        }
        public static string GetText(this PropertyInfo propertyInfo)
        {
            var propType = string.Empty;
            if (propertyInfo.PropertyType == typeof(String))
            {
                propType = "string";
            }
            else if (propertyInfo.PropertyType == typeof(Boolean))
            {
                propType = "bool";
            }
            else if (propertyInfo.PropertyType == typeof(Boolean?))
            {
                propType = "bool?";
            }
            else if (propertyInfo.PropertyType == typeof(DateTime))
            {
                propType = "DateTime?";
            }
            else if (propertyInfo.PropertyType == typeof(DateTime?))
            {
                propType = "DateTime?";
            }
            else if (propertyInfo.PropertyType == typeof(Guid))
            {
                propType = "Guid";
            }
            else if (propertyInfo.PropertyType == typeof(Guid?))
            {
                propType = "Guid?";
            }
            else if (propertyInfo.PropertyType == typeof(Int32))
            {
                propType = "int";
            }
            else if (propertyInfo.PropertyType == typeof(Int32?))
            {
                propType = "int?";
            }
            else if (propertyInfo.PropertyType == typeof(Double))
            {
                propType = "double";
            }
            else if (propertyInfo.PropertyType == typeof(Double))
            {
                propType = "double?";
            }
            else if (propertyInfo.PropertyType.IsEnum)
            {
                propType = "int";
            }
            else if (propertyInfo.PropertyType == typeof(Decimal))
            {
                propType = "decimal";
            }
            else if (propertyInfo.PropertyType == typeof(Decimal?))
            {
                propType = "decimal?";
            }
            else if (propertyInfo.PropertyType == typeof(Single))
            {
                propType = "float";
            }
            else if (propertyInfo.PropertyType == typeof(Single?))
            {
                propType = "float?";
            }

            if (string.IsNullOrEmpty(propType))
                return propType;

            var name = propertyInfo.Name;

            return $"public {propType} {name} " + "{ get; set; }";
        }
    }
}
