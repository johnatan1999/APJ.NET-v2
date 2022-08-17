using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Core.Page
{
    public class FormMetaData : MetaData
    {
        public readonly Dictionary<string, FormField> Fields;

        public FormMetaData(): base()
        {
            Fields = new Dictionary<string, FormField>();
        }

        public void AddField(string name)
        {
            Fields.Add(name, new FormField() { Name=name });
        }

        public void AddField(string name, string type)
        {
            var field = Fields[name];
            if (field == null)
                field = new FormField() { Name = name };
            field.Type = type;
            Fields.Add(name, field);
        }
        public void AddField(string name, string type, string label)
        {
            var field = Fields[name];
            if (field == null)
                field = new FormField() { Name = name };
            field.Type = type;
            field.Label = label;
            Fields.Add(name, field);
        }
        public void AddField(string name, string type, string label, object[] data)
        {
            var field = Fields[name];
            if (field == null)
                field = new FormField() { Name = name };
            field.Type = type;
            field.Label = label;
            field.Data = data;
            Fields.Add(name, field);
        }

        public void SetFieldType(string name, string type)
        {
            var field = Fields[name];
            if (field != null)
            {
                field.Type = type;
            }
        }
        public void SetFieldLabel(string name, string label)
        {
            var field = Fields[name];
            if (field != null)
            {
                field.Label = label;
            }
        }
        public void SetFieldData(string name, object[] data)
        {
            var field = Fields[name];
            if (field != null)
            {
                field.Data = data;
            }
        }

    }

    public class FormField
    {
        public string Name { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public object[] Data { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var tmp = (FormField)obj;
            return tmp.Name.Equals(this.Name);    
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
