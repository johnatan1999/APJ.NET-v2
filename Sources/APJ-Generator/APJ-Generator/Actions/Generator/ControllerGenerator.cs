using APJ.Generator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Actions
{
    public class ControllerGenerator : IAction
    {
        public ControllerGenerator()
        {
        }

        public override object Execute(string [] args)
        {
            validate(args.Length < 1, "Please specify a model to generate");
            string[] model = args[0].Split('.');
            string modelName = model[model.Length - 1];
            bool noState = args.Length > 1 && args[1].ToLower().Equals("ns");
            var templateName = noState ? "Controller-NS.txt" : "Controller.txt";
            string template = FileAccess.GetTemplate(templateName);
            template = template.Replace("{{Model_Name}}", modelName);
            template = template.Replace("{{Api_Path}}", modelName);
            template = template.Replace("{{Model_Package}}", FileAccess.GetModelPackage(args[0]));
            template = template.Replace("{{Namespace}}", FileAccess.GetProjectName() + ".Controllers");
            FileAccess.CreateController(modelName, template);
            return template;
        }

    }
}
