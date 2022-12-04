using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PluralizeService.Core;

namespace Project.CodeGenerator
{
    public class DbContextBuilder
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

                    #region dbset generate
                    var list = GetUndefinedEntitiesDbSetStrings(entities);
                    var text = GenerateDbSet(list);
                    string dbContext = File.ReadAllText(GeneralSetting.DbContextFilePath);
                    string insertPoint = "dont remove this line";
                    int index = dbContext.IndexOf(insertPoint) + insertPoint.Length;
                    dbContext = dbContext.Insert(index, text);
                    File.WriteAllText(GeneralSetting.DbContextFilePath, dbContext);
                    #endregion

                    #region usings generate
                    list = GetUndefinedEntitiesNamespaces(entities);
                    text = GenerateUsings(list);
                    dbContext = File.ReadAllText(GeneralSetting.DbContextFilePath);
                    insertPoint = "Auto generate usings";
                    index = dbContext.IndexOf(insertPoint) + insertPoint.Length;
                    dbContext = dbContext.Insert(index, text);
                    File.WriteAllText(GeneralSetting.DbContextFilePath, dbContext);
                    #endregion
                }
            }
        }

        private static List<string> GetUndefinedEntitiesDbSetStrings(List<Type> entities)
        {
            var list = new List<string>();
            using(var reader = new StreamReader(GeneralSetting.DbContextFilePath))
            {
                var text = reader.ReadToEnd();
                foreach(var entity in entities)
                {
                    var dbSet = $"DbSet<{entity.Name}>";
                    if (!text.Contains(dbSet))
                    {
                        list.Add(entity.Name);
                    }
                }
            }

            return list;
        }

        private static List<string> GetUndefinedEntitiesNamespaces(List<Type> entities)
        {
            var list = new List<string>();
            using (var reader = new StreamReader(GeneralSetting.DbContextFilePath))
            {
                var text = reader.ReadToEnd();
                foreach (var entity in entities)
                {
                    var _namespace = $"using {entity.Namespace};";
                    if (!text.Contains(entity.Namespace))
                    {
                        list.Add(_namespace);
                    }
                }
            }

            return list;
        }

        private static string GenerateDbSet(List<string> list)
        {
            var builder = new StringBuilder();
            foreach (var item in list)
            {
                var pluralItem = PluralizationProvider.Pluralize(item);
                var dbSet = "       public DbSet<" + item + "> " +pluralItem + "{ get; set; }";
                builder.AppendLine();
                builder.AppendLine(dbSet);
            }
            return builder.ToString();
        }
        private static string GenerateUsings(List<string> list)
        {
            var builder = new StringBuilder();
            foreach (var item in list.Distinct())
            {
                builder.AppendLine();
                builder.AppendLine(item);
            }
            return builder.ToString();
        }
    }
}
