using Abp.Domain.Entities;
using Project.Souccar.Domain.DomainModel;
using Project.Souccar.Global.Constant;
using System;
using System.Reflection;

namespace Project.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Files : ");
            var assembly = typeof(SouccarAggregate).Assembly;
            var modules = new List<string>();
            Type modulesNames = typeof(ModulesNames);
            FieldInfo[] fields = modulesNames.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo fi in fields)
            {
                modules.Add(fi.GetValue(null).ToString());
            }

            Console.Read();
            ModulesBuilder.Generate(assembly, modules);
            LocalizationBuilder.Generate(assembly, modules);
            DbContextBuilder.Generate(assembly, modules);
            PermissionsBuilder.Generate(assembly, modules);

            Console.WriteLine("Files : " + GeneralSetting.FileCount);
        }
    }
}
