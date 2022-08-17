using APJ.Generator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APJ.Generator.Actions
{
    internal class UserModuleGenerator : IAction
    {
        public override object Execute(string[] args)
        {
            string modules = "";
            var _modules = FileAccess.GetExistingModuleName();
            foreach (string module in _modules) modules += "- " + module + "\n";
            validate(args.Length < 1, "Please specify one of these the following modules:\n"+modules);
            string moduleName = Util.FirstLetterToUpper(args[0]);
            string destinationFolder = "./Models/"+moduleName+"/";
            FileAccess.CreateDirectory(destinationFolder);
            FileAccess.CopyModuleModels(moduleName, destinationFolder);
            return null;
        }
    }
}
