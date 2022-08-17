using APJ.Generator.Actions;
using APJ.Generator.Actions.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator
{
    internal class Generator
    {

        public static  string[] generableObject =  {"all", "controller", "service"};

        private static string GenerableObjectToString()
        {
            string tmp = "";
            foreach(var s in generableObject)
            {
                tmp += "\t" + s + "\n";
            }
            return tmp;
        }

        public static void Generate(string [] parameters)
        {
            if (parameters.Length < 1) throw new Exception("Please specify an object to generate\n"+GenerableObjectToString());
            string objectToGenerate = parameters[0];
            switch(objectToGenerate.ToLower())
            {
                case "controller":
                    GenerateController(parameters.Skip(1).ToArray());
                    break;
                case "service":
                    GenerateService(parameters.Skip(1).ToArray());
                    break;
                case "model":
                    GenerateModel(parameters.Skip(1).ToArray());
                    break;
                case "all":
                    GenerateModel(parameters.Skip(1).ToArray());
                    GenerateController(parameters.Skip(1).ToArray());
                    GenerateService(parameters.Skip(1).ToArray());
                    break;
                default: break;
            }
        }

        public static void GenerateController(string [] parameters)
        {
            var action = new ControllerGenerator();
            action.Execute(parameters);
        }

        public static void GenerateModule(string [] parameters)
        {
            var action = new UserModuleGenerator();
            action.Execute(parameters);
        }

        public static void GenerateService(string [] parameters)
        {
            var action = new ServiceGenerator();
            action.Execute(parameters);
        }

        public static void GenerateModel(string [] parameters)
        {
            IAction action = new MapGenerator();
            try
            {
                action.Execute(parameters);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                action = new ModelGenerator();
                action.Execute(parameters);
            }
        }

        public static void MapModel(string [] parameters)
        {
            var action = new MapGenerator();
            action.Execute(parameters);
        }

    }
}
