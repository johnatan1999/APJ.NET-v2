using APJ.Generator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Actions
{
    public class ServiceGenerator : IAction
    {
        public override object Execute(string[] args)
        {
            validate(args.Length < 1, "Please specify a model to generate");
            string[] model = args[0].Split('.');
            string modelName = model[model.Length - 1];
            bool noState = args.Length > 1 && args[1].ToLower().Equals("ns");
            var templateName = noState  ? "Service-NS.txt" : "Service.txt";
            string template = FileAccess.GetTemplate(templateName);
            template = template.Replace("{{Model_Name}}", modelName);
            template = template.Replace("{{Namespace}}", FileAccess.GetProjectName()+".Services");
            var modelPackage = FileAccess.GetModelPackage(args[0]);
            var modelRoot = FileAccess.GetModelPackage("");
            if (!modelPackage.Equals(modelRoot) && !noState) {
                modelPackage += ";\nusing " + modelRoot;
            }
            template = template.Replace("{{Model_Package}}", modelPackage);
            FileAccess.CreateService(modelName, template);
            return template;
        }
    }
}
