using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Utility
{
    public class FileAccess
    {

        public static string GetAppRootDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
        }

        public static string GetTemplate(string name)
        {
            return File.ReadAllText(Path.Combine(GetAppRootDirectory(), "Templates/" + name));
        }

        public static void CopyModuleModels(string moduleName, string destFolder)
        {
            string [] filesPath = Directory.GetFiles(Path.Combine(GetAppRootDirectory(), "Modules/" + moduleName));
            foreach(string filePath in filesPath)
            {
                File.Copy(filePath, destFolder+Path.GetFileName(filePath.Replace("txt", "cs")));
            }
            Console.WriteLine(moduleName + " module copied.");
        }

        public static List<string> GetExistingModuleName()
        {
            string[] modulesPath = Directory.GetDirectories(Path.Combine(GetAppRootDirectory(), "Modules/"));
            List<string> modulesName = new List<string>();
            foreach (string modulePath in modulesPath)
                modulesName.Add(Path.GetDirectoryName(modulePath));
            return modulesName;
        } 

        public static void CreateController(string name, string controller)
        {
            var _name = name; // !name.EndsWith("s") ? name + "s" : name; 
            if (File.Exists("./Controllers/" + _name + "Controller.cs"))
                Console.WriteLine("A controller with the name " + _name + " already exists");
            else
            { 
                File.WriteAllText("./Controllers/" + _name + "Controller.cs", controller);
                Console.WriteLine(_name + "Controller.cs created");
            }
        }
        
        public static void CreateService(string name, string service)
        {
            if (File.Exists("./Services/" + name + "Service.cs")) 
                Console.WriteLine("A service with the name " + name + " already exists");
            else
            {
                CreateDirectory("./Services");
                File.WriteAllText("./Services/" + name + "Service.cs", service);
                Console.WriteLine(name + "Service.cs created");
            }
        }
        public static void CreateModel(string name, string model)
        {
            if (File.Exists("./Models/" + name + ".cs"))
                Console.WriteLine("A model with the name " + name + " already exists");
            else
            {
                CreateDirectory("./Models");
                File.WriteAllText("./Models/" + name + ".cs", model);
                Console.WriteLine(name + ".cs created");
            }
        }

        public static void CreateModule(string name, string data)
        {
            File.WriteAllText("./Models/" + name, data);
        }

        public static bool DirectoryExist(string path)
        {
            return Directory.Exists(path);
        }


        public static void CreateControllerDirectory()
        {
            if(!Directory.Exists("./Controllers"))
            {
                Directory.CreateDirectory("Controllers");
            }
        }

        public static string CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return Directory.CreateDirectory(directory).FullName;
            }
            return directory;
        }

        public static string GetProjectName()
        {
            return Path.GetFileName(Environment.CurrentDirectory).Replace(" ", ".");
        }

        public static string GetModelPackage(string model)
        {
            string[] tmp = model.Split('.');
            string package = "";
            for(int i=0; i<tmp.Length-1; i++)
                package += "."+tmp[i];
            return GetProjectName()+".Models" + package;
        }
    }
}
