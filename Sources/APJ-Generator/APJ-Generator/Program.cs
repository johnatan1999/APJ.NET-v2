using APJ.Generator.Actions;
using APJ.Generator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator
{
    class Program
    {


        static void Main(string[] args)
        {
            string[] actionList = new string[]
            {
                "generate", "g", "add-module", "map"
            };

            try
            {
                bool controllerDirectoryExist = FileAccess.DirectoryExist("/Controllers");
                FileAccess.CreateControllerDirectory();
                if (true)
                {
                    Console.WriteLine("Current folder="+ Environment.CurrentDirectory);
                    string actionName = args[0];
                    var parameters = args.Skip(1).ToArray();
                    string actions = "";
                    foreach (string action in actionList) actions += "- " + action + "\n";
                    if(args.Length < 1) throw new Exception("Please specify one of these action name \n- generate");
                    switch (actionName.ToLower())
                    {
                        case "g":
                            Generator.Generate(parameters);
                            break;
                        case "generate":
                            Generator.Generate(parameters);
                            break;
                        case "add-module":
                            Generator.GenerateModule(parameters);
                            break;
                        case "map":
                            Generator.MapModel(parameters);
                            break;
                        default:
                            Console.WriteLine("Unknown action.\nPlease specify one of the following action names \n"+ actions);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw e;
            }
            finally
            {
                //Console.ReadLine();
            }
        }
    }
}
