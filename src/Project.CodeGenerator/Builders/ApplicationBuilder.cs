using Project.CodeGenerator.Builders.Advice;
using System;
using System.IO;
using System.Threading;

namespace Project.CodeGenerator
{
    public class ApplicationBuilder
    {
        public static void Genetate(Type entityType)
        {
            var folderPath = entityType.Namespace.ApplicationEntityPath();

            GenerateDtos(entityType, folderPath);
            GenerateMap(entityType, folderPath);


        }

        
        #region Dto
        public static void GenerateDtos(Type entityType, string folderPath)
        {
            var dtoPath = Path.Combine(folderPath, "Dto");
            if (!Directory.Exists(dtoPath))
                Directory.CreateDirectory(dtoPath);

            GenerateDtoFile(entityType, dtoPath);
            GenerateDtoFile(entityType, dtoPath, "Read");
            GenerateDtoFile(entityType, dtoPath, "Update");
            GenerateDtoFile(entityType, dtoPath, "Create");
            GenerateAppService(folderPath, entityType);
            GenerateAdvice(folderPath, entityType);
        }

        private static void GenerateAppService(string folderPath, Type entityType)
        {
            var path = Path.Combine(folderPath, "Services");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Class
            var classFileName = $"{entityType.Name}AppService.cs";
            var classFilePath = Path.Combine(path, classFileName);

            if (!File.Exists(classFilePath))
            {
                var builder = new ImplementAppServiceBuilder(entityType);
                var classText = builder.Genetate();

                CreateCsFile(classText, classFilePath);
            }

            //Interfcae
            var interfaceFileName = $"I{entityType.Name}AppService.cs";
            var interfaceFilePath = Path.Combine(path, interfaceFileName);

            if (!File.Exists(interfaceFilePath))
            {
                var builder = new InterfaceAppServiceBuilder(entityType);
                var interfaceText = builder.Genetate();

                CreateCsFile(interfaceText, interfaceFilePath);
            }
        }
        private static void GenerateAdvice(string folderPath, Type entityType)
        {
            var path = Path.Combine(folderPath, "Advices");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Class
            var classFileName = $"{entityType.Name}Advice.cs";
            var classFilePath = Path.Combine(path, classFileName);

            if (!File.Exists(classFilePath))
            {
                var builder = new ImplementAdviceBuilder(entityType);
                var classText = builder.Genetate();

                CreateCsFile(classText, classFilePath);
            }

            //Interfcae
            var interfaceFileName = $"I{entityType.Name}Advice.cs";
            var interfaceFilePath = Path.Combine(path, interfaceFileName);

            if (!File.Exists(interfaceFilePath))
            {
                var builder = new InterfaceAdviceBuilder(entityType);
                var interfaceText = builder.Genetate();

                CreateCsFile(interfaceText, interfaceFilePath);
            }
        }

        private static void GenerateDtoFile(Type entityType, string dtoPath, string prefix = "")
        {

            var fileName = $"{prefix}{entityType.Name}Dto.cs";
            var path = Path.Combine(dtoPath, fileName);

            if (!File.Exists(path))
            {
                var dtoBuilder = new DtoBuilder(entityType, prefix);
                var text = dtoBuilder.Genetate();

                CreateCsFile(text, path);
            }
            
        }
        #endregion

        #region Map
        private static void GenerateMap(Type entityType, string folderPath)
        {
            var dtoPath = Path.Combine(folderPath, "Map");
            if (!Directory.Exists(dtoPath))
                Directory.CreateDirectory(dtoPath);

            var fileName = $"{entityType.Name}MapProfile.cs";
            var path = Path.Combine(dtoPath, fileName);

            if (!File.Exists(path))
            {
                var dtoBuilder = new MapBuilder(entityType);
                var text = dtoBuilder.Genetate();

                CreateCsFile(text, path);
            }
        }
        #endregion

        private static void CreateCsFile(string text, string path)
        {
            var sw = new StreamWriter(path);
            sw.WriteLine(text);
            sw.Close();
            Thread.Sleep(50);

            Console.WriteLine(path);
            GeneralSetting.FileCount++;
        }
    }
}
