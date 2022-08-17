using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Core.Page
{
    public abstract class MetaData
    {
        public IList<string> HiddenAttributes { get; }

        public MetaData()
        {
            HiddenAttributes = new List<string>();
        }

        public void HideAttribute(string attributeName)
        {
            if(!HiddenAttributes.Contains(attributeName))
                HiddenAttributes.Add(attributeName);
        }

        public void ShowAttribute(string attributeName)
        {
            HiddenAttributes.Remove(attributeName);
        }
    }
}
