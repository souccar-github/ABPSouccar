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
                        var page = $"Pages_{entity.Name.Plural()}";
                        var create = $"Actions_{entity.Name.Plural()}_Create";
                        var update = $"Actions_{entity.Name.Plural()}_Update";
                        var delete = $"Actions_{entity.Name.Plural()}_Delete";

                        var permissionViewModel = new PermissionViewModel()
                        {
                            Entity = entity.Name,
                            Page = !text.Contains(page) ? false : true,
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

        private static Tuple<string,string> GeneratePermissions(List<PermissionViewModel> list)
        {
            var permissionBuilder = new StringBuilder();
            var providerBuilder = new StringBuilder();
            foreach (var item in list)
            {
                permissionBuilder.AppendLine();
                permissionBuilder.AppendLine("        //" + item.Entity.Plural());

                providerBuilder.AppendLine();
                providerBuilder.AppendLine("            //" + item.Entity.Plural());

                if (!item.Page)
                {
                    var pageStr = $"Pages_{item.Entity.Plural()}";
                    permissionBuilder.AppendLine($"        public const string {pageStr} = \"Pages.{item.Entity.Plural()}\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{pageStr}, L(\"{item.Entity}\"));");
                }

                if(!item.Create)
                {
                    var pageStr = $"Actions_{item.Entity.Plural()}_Create";
                    permissionBuilder.AppendLine($"        public const string {pageStr} = \"Actions.{item.Entity.Plural()}.Create\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{pageStr}, L(\"CreateNew{item.Entity}\"));");
                }

                if (!item.Update)
                {
                    var pageStr = $"Actions_{item.Entity.Plural()}_Update";
                    permissionBuilder.AppendLine($"        public const string {pageStr} = \"Actions.{item.Entity.Plural()}.Update\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{pageStr}, L(\"Edit{item.Entity}\"));");
                }

                if (!item.Delete)
                {
                    var pageStr = $"Actions_{item.Entity.Plural()}_Delete";
                    permissionBuilder.AppendLine($"        public const string {pageStr} = \"Actions.{item.Entity.Plural()}.Delete\";");
                    providerBuilder.AppendLine($"            context.CreatePermission(PermissionNames.{pageStr}, L(\"Delete{item.Entity}\"));");
                }
            }
            return Tuple.Create(permissionBuilder.ToString(), providerBuilder.ToString());
        }
    }

    internal class PermissionViewModel
    {
        public string Entity { get; set; }
        public bool Page { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Update { get; set; }
    }
}
