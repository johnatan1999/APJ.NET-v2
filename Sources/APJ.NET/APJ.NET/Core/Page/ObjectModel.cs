using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Core.Page
{

    public class ObjectModelField
    {
        public string Type { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public object [] Data { get; set; }
        public string DataFieldLabel { get; set; }
    }
    public class ObjectModel
    {
        public IList<ObjectModelField> Fields { get; }

        public ObjectModel()
        {
            Fields = new List<ObjectModelField>();
        }
        public void Add(string name, string type)
        {
            Fields.Add(new ObjectModelField() { 
                Name = name,
                Label = name,
                Type = type
            });
        }
        public void Add(string name, string type, object value)
        {
            Fields.Add(new ObjectModelField()
            {
                Name = name,
                Label = name,
                Type = type,
                Value = value
            });
        }

        public void Add(ObjectModelField model)
        {
            Fields.Add(model);
        }

        public void Add(string name, string type, object value, object [] data)
        {
            Fields.Add(new ObjectModelField()
            {
                Name = name,
                Label = name,
                Type = type,
                Value = value,
                Data = data
            });
        }
        
        public void Add(string name, string type, object value, object [] data, string dataFieldLabel)
        {
            Fields.Add(new ObjectModelField()
            {
                Name = name,
                Label = name,
                Type = type,
                Value = value,
                Data = data,
                DataFieldLabel=dataFieldLabel
            });
        }
    }
}
