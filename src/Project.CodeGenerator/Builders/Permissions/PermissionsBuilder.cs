using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Abp.Domain.Entities;
using System.Drawing.Drawing2D;

namespace Project.CodeGenerator
{
    internal class PermissionsBuilder
    {
        public static void Generate(Assembly assembly, List<string> modules)
        {
            foreach (var moduleName in modules)
            {
                Console.WriteLine($"Module : {moduleName}");
                var entities = assembly.GetTypes()
                .Where(t => 
                t.Namespace != null
                && t.Namespace.Contains($"{GeneralSetting.ProjectName}.{moduleName}")
                && t.BaseType != null
                && (t.BaseType.Name.Contains("SouccarAggregate") || t.BaseType.Name.Contains("SouccarIndex") || t.BaseType.Name.Contains("SouccarEntity"))
                && t.IsClass == true).ToList();


                if (entities.Any())
                {
                    var list = GetUndefinedEntities(entities);
                    
                    var text = GeneratePermissions(list);
                    #region permission names generate
                    var permissoinNamesText = text.Item1;
                    var permissoinNames = File.ReadAllText(GeneralSetting.PermissionsNamesFilePath);
                    var insertPoint = "dont remove this line";
                    var index = permissoinNames.IndexOf(insertPoint) + insertPoint.Length;
                    permissoinNames = permissoinNames.Insert(index, permissoinNamesText);
                    File.WriteAllText(GeneralSetting.PermissionsNamesFilePath, permissoinNames);
                    #endregion

                    #region permission names generate
                    var permissoinProviderText = text.Item2;
                    var permissoinProvider = File.ReadAllText(GeneralSetting.PermissionsProviderFilePath);
                    insertPoint = "dont remove this line";
                    index = permissoinProvider.IndexOf(insertPoint) + insertPoint.Length;
                    permissoinProvider = permissoinProvider.Insert(index, permissoinProviderText);
                    File.WriteAllText(GeneralSetting.PermissionsProviderFilePath, permissoinProvider);
                    #endregion
                }
            }
        }
        private static List<PermissionViewModel> GetUndefinedEntities(List<Type> entities)
        {
            var list = new List<PermissionViewModel>();
            using (var reader = new StreamReader(GeneralSetting.PermissionsNamesFilePath))
            {
                var text = reader.ReadToEnd();
                foreach (var entity in entities)
                {
                    if (!text.Contains(entity.Name.Plural()))
                    {
                        var module = entity.FullName.Split('.')[1];
                        var permType = entity.FullName.Split('.')[2];
                        var read = $"{module}_{permType}_Read_{entity.Name.Plural()}";
                        var create = $"{module}_{permType}_Insert_{entity.Name.Plural()}";
                        var update = $"{module}_{permType}_Edit_{entity.Name.Plural()}";
                        var delete = $"{module}_{permType}_Delete_{entity.Name.Plural()}";

                        var permissionViewModel = new PermissionViewModel()
                        {
                            Entity = entity,
                            Read = !text.Contains(read) ? false : true,
                            Create = !text.Contains(create) ? false : true,
                            Update = !text.Contains(update) ? false : true,
                            Delete = !text.Contains(delete) ? false : true
                        };
                        list.Add(permissionViewModel);
                    }
                }
            }
            return list;
        }

        private static string GetPermissionString(string str)
        {
            return str.Replace('_','.');
        }

        private static Tuple<string,string> GeneratePermissions(List<PermissionViewModel> list)
        {
            var permissionBuilder = new StringBuilder();
            var providerBuilder = new StringBuilder();
            foreach (var item in list)
            {
                var entity = item.Entity;
                var module = entity.FullName.Split('.')[1];
                var permType = entity.FullName.Split('.')[2];
                var read = $"{module}_{permType}_Read_{entity.Name.Plural()}";
                var create = $"{module}_{permType}_Insert_{entity.Name.Plural()}";
                var update = $"{module}_{permType}_Edit_{entity.Name.Plural()}";
                var delete = $"{module}_{permType}_Delete_{entity.Name.Plural()}";

                permissionBuilder.AppendLine();
                permissionBuilder.AppendLine("        //" + entity.Name.Plural());

                providerBuilder.AppendLine();
                providerBuilder.AppendLine("            //" + entity.Name.Plural());

                if (!item.Read)
                {
                    var str = $"{module}_{permType}_Read_{entity.Name.Plural()}";
                    var permissionString = GetPermissionString(str);
                    permissionBuilder.AppendLine($"        public const string {str} =\"{permissionString}\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{str}, L(\"{entity.Name}\"));");
                }

                if(!item.Create)
                {
                    var str = $"{module}_{permType}_Insert_{entity.Name.Plural()}";
                    var permissionString = GetPermissionString(str);
                    permissionBuilder.AppendLine($"        public const string {str} =\"{permissionString}\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{str}, L(\"CreateNew{entity.Name}\"));");
                }

                if (!item.Update)
                {
                    var str = $"{module}_{permType}_Edit_{entity.Name.Plural()}";
                    var permissionString = GetPermissionString(str);
                    permissionBuilder.AppendLine($"        public const string {str} = \"{permissionString}\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{str}, L(\"Edit{entity.Name}\"));");
                }

                if (!item.Delete)
                {
                    var str = $"{module}_{permType}_Delete_{entity.Name.Plural()}";
                    var permissionString = GetPermissionString(str);
                    permissionBuilder.AppendLine($"        public const string {str} = \"{permissionString}\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{str}, L(\"Delete{entity.Name}\"));");
                }
            }
            return Tuple.Create(permissionBuilder.ToString(), providerBuilder.ToString());
        }
    }

    internal class PermissionViewModel
    {
        public Type Entity { get; set; }
        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Update { get; set; }
    }
}
