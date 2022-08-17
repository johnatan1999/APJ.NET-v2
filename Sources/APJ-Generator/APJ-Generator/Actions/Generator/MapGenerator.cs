using APJ.Generator.Core.Mapping;
using APJ.Generator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Actions.Generator
{
    public class MapGenerator : IAction
    {
        public override object Execute(string[] args)
        {
            validate(args.Length < 1, "Please specify a model to generate");
            string[] model = args[0].Split('.');
            string modelName = model[model.Length - 1];
            bool noState = args.Length > 1 && args[1].ToLower().Equals("ns");
            var templateName = noState ? "Model-NS.txt" : "Model.txt";
            string template = FileAccess.GetTemplate(templateName);
            template = template.Replace("{{Model_Name}}", modelName);
            template = template.Replace("{{Namespace}}", FileAccess.GetProjectName() + ".Models");
            template = ApjORM.ConvertTableToClass(modelName, template);
            FileAccess.CreateModel(args[0].Replace(".", "/"), template);
            return template;
        }

    }
}
